#pragma once

template<typename T>
constexpr bool isEven(T arg)
{
    return (arg & 1) == 0;
}

template<typename T>
constexpr bool isOdd(T arg)
{
    return (arg & 1) != 0;
}

template<typename T>
T gcd(T u, T v)
{
    // https://en.wikipedia.org/wiki/Binary_GCD_algorithm

    int shift;

    /* GCD(0,v) == v; GCD(u,0) == u, GCD(0,0) == 0 */
    if (u == 0) return v;
    if (v == 0) return u;

    /* Let shift := lg K, where K is the greatest power of 2
    dividing both u and v. */
    for (shift = 0; isEven(u | v); ++shift) {
        u >>= 1;
        v >>= 1;
    }

    while (isEven(u))
        u >>= 1;

    /* From here on, u is always odd. */
    do {
        /* remove all factors of 2 in v -- they are not common */
        /*   note: v is not zero, so while will terminate */
        while (isEven(v))  /* Loop X */
            v >>= 1;

        /* Now u and v are both odd. Swap if necessary so u <= v,
        then set v = v - u (which is even). For bignums, the
        swapping is just pointer movement, and the subtraction
        can be done in-place. */
        if (u > v) {
            auto t = v; v = u; u = t;
        }  // Swap u and v.
        v = v - u;                       // Here v >= u.
    } while (v != 0);

    /* restore common factors of 2 */
    return u << shift;
}

template<typename T>
T gcd_multi(T *v, int size)
{
	T acc = v[0];
	for (int i = 1; i < size; ++i)
		acc = gcd(acc, v[i]);
	return acc;
}

template<typename T>
T lcm(T u, T v)
{
    // https://en.wikipedia.org/wiki/Least_common_multiple

    return u / gcd<T>(u, v) * v;
}

template<typename T>
T lcm_multi(T *v, int size)
{
	T acc = v[0];
	for (int i = 1; i < size; ++i)
		acc = lcm(acc, v[i]);
	return acc;
}

template<typename T>
constexpr T int_sqrt(T n)
{
    // AREA-EFFICIENT IMPLEMENTATION OF A FAST SQUARE ROOT ALGORITHM
    // Matti T. Tommiska
    // http://lib.tkk.fi/Diss/2005/isbn9512275279/article3.pdf
    T mask = static_cast<T>(1) << ((sizeof(n) * 8) - 2);
    T root = 0;
    T remainder = n;

    while (mask != 0)
    {
        if ((root + mask) <= remainder)
        {
            remainder -= (root + mask);
            root += (mask << 1);
        }
        root >>= 1;
        mask >>= 2;
    }
    return root;
}

template<typename T>
constexpr bool is_perfect_square(T n)
{
    T mask = static_cast<T>(1) << ((sizeof(n) * 8) - 2);
    T root = 0;
    T remainder = n;

    while (mask != 0)
    {
        if ((root + mask) <= remainder)
        {
            remainder -= (root + mask);
            root += (mask << 1);
        }
        root >>= 1;
        mask >>= 2;
    }
    return remainder == 0;
}