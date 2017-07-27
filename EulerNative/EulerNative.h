// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the EULERNATIVE_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// EULERNATIVE_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef EULERNATIVE_EXPORTS
#define EULERNATIVE_API __declspec(dllexport)
#else
#define EULERNATIVE_API __declspec(dllimport)
#endif

#include "NativeTemplates.h"

#ifdef __cplusplus
extern "C" {
#endif

//// This class is exported from the EulerNative.dll
//class EULERNATIVE_API CEulerNative {
//public:
//	CEulerNative(void);
//	// TODO: add your methods here.
//};
//
//extern EULERNATIVE_API int nEulerNative;


template EULERNATIVE_API uint64_t gcd(uint64_t u, uint64_t v);
template EULERNATIVE_API uint64_t gcd_multi(uint64_t *u, int size);
template EULERNATIVE_API uint64_t lcm(uint64_t u, uint64_t v);
template EULERNATIVE_API uint64_t lcm_multi(uint64_t *u, int size);

template EULERNATIVE_API uint64_t int_sqrt(uint64_t n);
template EULERNATIVE_API bool is_perfect_square(uint64_t n);



#ifdef __cplusplus
} /* extern "C" */
#endif