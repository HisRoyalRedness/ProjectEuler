using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections;
using System.Windows.Input;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows;

#if REACTIVE
using System.Reactive.Subjects;
using System.Reactive.Linq;
#endif

/*
    General class for property change notification

    Keith Fletcher
    May 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    public abstract class NotifyBase : NotifyBase<object>
    {
        protected NotifyBase(object propertyLock = null)
            : base(propertyLock)
        { }
    }

    public abstract class NotifyBase<TLock> : INotifyPropertyChanged
        where TLock : class
    {
        protected NotifyBase(TLock propertyLock = null)
        {
            _propertyLock = propertyLock;
        }

        public event PropertyChangedEventHandler PropertyChanged;

#if REACTIVE
        readonly Subject<string> _changes = new Subject<string>();

        public IObservable<string> PropertyChanges
        { get { return _changes; } }

        /// <summary>
        /// Project all echoFrom properties onto echoTo. 
        /// This has the effect of making echoFrom changes appears as echoTo changes as well.
        /// </summary>
        protected void EchoChanges(string echoTo, params string[] echoFrom)
        {
            _changes.Where(prop => echoFrom.Contains(prop)).Subscribe(prop => NotifyPropertyChanged(echoTo));
        }
#endif

        protected TValue GetProperty<TValue>(ref TValue propertyMember)
        {
            if (_propertyLock == null)
                return propertyMember;

            TValue value;
            lock (_propertyLock)
                value = propertyMember;
            return value;
        }

        //[DebuggerStepThrough]
        protected bool SetProperty<TValue>(ref TValue propertyMember, TValue newValue, Action<TValue> actionIfChanged = null, [CallerMemberName]string propertyName = "")
            => SetProperty(ref propertyMember, newValue, null, actionIfChanged, propertyName);


        /// <summary>
        /// Attempt to set the property. If the given value is different from the current value,
        /// a PropertyChanged event will be raised.
        /// </summary>
        protected bool SetProperty<TValue>(ref TValue propertyMember, TValue newValue, Dispatcher dispatcher, Action<TValue> actionIfChanged = null, [CallerMemberName]string propertyName = "")
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            var hasChanged = false;
            if (_propertyLock == null)
            {
                hasChanged = HasChanged(ref propertyMember, newValue);
                if (hasChanged)
                    propertyMember = newValue;
            }
            else
            {
                lock (_propertyLock)
                {
                    hasChanged = HasChanged(ref propertyMember, newValue);
                    if (hasChanged)
                        propertyMember = newValue;
                }
            }

            if (hasChanged)
            {
                if (dispatcher == null || dispatcher == Dispatcher.CurrentDispatcher)
                {
                    NotifyPropertyChanged(propertyName);
                    if (actionIfChanged != null)
                        actionIfChanged(newValue);
                }
                else
                {
                    dispatcher.InvokeAsync(() =>
                    {
                        NotifyPropertyChanged(propertyName);
                        if (actionIfChanged != null)
                            actionIfChanged(newValue);
                    });
                }
            }

            return hasChanged;
        }


        protected Task<bool> SetPropertyAsync<TValue>(ref TValue propertyMember, TValue newValue, Dispatcher dispatcher, Action<TValue> actionIfChanged = null, [CallerMemberName]string propertyName = "")
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            var hasChanged = false;
            if (_propertyLock == null)
            {
                hasChanged = HasChanged(ref propertyMember, newValue);
                if (hasChanged)
                    propertyMember = newValue;
            }
            else
            {
                lock (_propertyLock)
                {
                    hasChanged = HasChanged(ref propertyMember, newValue);
                    if (hasChanged)
                        propertyMember = newValue;
                }
            }

            if (hasChanged)
            {
                if (dispatcher == null || dispatcher == Dispatcher.CurrentDispatcher)
                {
                    NotifyPropertyChanged(propertyName);
                    if (actionIfChanged != null)
                        actionIfChanged(newValue);
                }
                else
                {
                    return dispatcher.InvokeAsync(() =>
                    {
                        NotifyPropertyChanged(propertyName);
                        if (actionIfChanged != null)
                            actionIfChanged(newValue);
                        return true;
                    }).Task;
                }
            }

            return Task.FromResult(hasChanged);
        }

        bool HasChanged<TValue>(ref TValue propertyMember, TValue newValue)
            => !(((propertyMember == null && newValue == null) ||
                (propertyMember != null && propertyMember.Equals(newValue))));


        /// <summary>
        /// Raise a PropertyChanged event for this given properties
        /// </summary>
        protected void NotifyPropertyChanged(params string[] properties)
        {
            foreach (var property in properties)
            {
#if REACTIVE
                _changes.OnNext(property);
#endif
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public bool IsInDesigner => DesignerProperties.GetIsInDesignMode(new DependencyObject());

        protected readonly TLock _propertyLock = null;

        public class RelayCommand : RelayCommand<object>
        {
            #region Constructors
            public RelayCommand(Action execute)
                : base(_ => execute(), null)
            { }

            public RelayCommand(Action<object> execute)
                : base(execute, null)
            { }

            public RelayCommand(Action<object> execute, Predicate<object> canExecute)
                : base(execute, canExecute)
            { }
            #endregion // Constructors
        }

        public class RelayCommand<TParam> : ICommand
            where TParam : class
        {
            #region Fields

            readonly Action<TParam> ExecuteAction;
            readonly Predicate<TParam> CanExecutePredicate;

            #endregion // Fields

            #region Constructors

            public RelayCommand(Action execute)
                : this(_ => execute(), null)
            { }

            public RelayCommand(Action<TParam> execute)
                : this(execute, null)
            { }

            public RelayCommand(Action<TParam> execute, Predicate<TParam> canExecute)
            {
                ExecuteAction = execute;
                CanExecutePredicate = canExecute;
            }
            #endregion // Constructors

            #region ICommand Members
            event EventHandler ICommand.CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            [DebuggerStepThrough]
            bool ICommand.CanExecute(object parameter) => (CanExecutePredicate == null ? true : CanExecutePredicate(parameter as TParam));

            public void Execute(object parameter) => ExecuteAction(parameter as TParam);
            #endregion // ICommand Members
        }
    }
}

/*
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org>
*/
