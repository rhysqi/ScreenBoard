CC		=	clang

OPT_F	=	-fenable-matrix
OPT_F	+=	-fstack-protector-strong -fvectorize

OPT_Fno	=	-fno-builtin -fno-convergent-functions -fno-courotines
OPT_Fno	+=	-fno-cxx-modules -fno-experimental-library -fno-exceptions
OPT_Fno	+=	-fno-strict-aliasing

OPT_M	=	-mavx
OPT_M	+=	-mlvi-cfi -mlvi-hardening
OPT_M	+=	-msese -msse4.2
OPT_M	+=	-mstack-arg-probe -mstackrealign

OPT_Mno	=	-mno-sse2 -mno-sse3
OPT_Mno	+=	-mno-outline-atomics -mno-restrict-it

OPT_Wl	=	-Wl",/DYNAMICBASE:YES"
OPT_Wl	+=	-Wl",/LARGEADDRESSNAME:YES"

OPT_Uni	=	-v

LIB_C	=	
LIB_C	+=	