! Last time we gave you the first dreamcast 4k ever. Today, it is time
! for me to give you the first ever dreamcast 256b intro!
! wiiiiieeeee! :)
!
! and it's also the first 256b ever done by me... wiiiieee!
!
! have fun! (and this sucks btw ;))
!
! .quarn/Outbreak - quarn@home.se - 19 july 2002 01:24:15
!
! To compile:
!	sh-elf-as -little -o 256b.o 256b.s
!	sh-elf-ld -EL --oformat srec -Ttext 0x8c010000 256b.o -o 256b.srec
!	sh-elf-objcopy -O binary 256b.srec 256b.bin

		.text

		.align	2
clrscr:
		mov.l	VIDEO_BASE,r0	! clear screen
		mov	#0,r1
		mov.l	CLRCOUNT,r2
clrscr_loop:
		mov.l	r1,@r0
		dt	r2
		bf/s	clrscr_loop
		add	#4,r0

xorgen:
		mov.l	TEXTURE,r1	! generate xor-texture
		mov.l	TWIDTH,r2
xor_yloop:
		mov	r2,r5
		shlr2	r5
		mov.l	TWIDTH,r3
xor_xloop:
		mov	r3,r4
		shlr2	r4
		xor	r5,r4
		mov.b	r4,@r1
		add	#1,r1
		dt	r3
		bf	xor_xloop

		add	r6,r1
		dt	r2
		bf	xor_yloop

		bra	main
		nop

main:
		mov	#127,r7
		mov	#0,r9
main_begin:
		mov.l	HEIGHT,r1
		add	#1,r9
main_yloop:
		mov.l	WIDTH,r2
		mov	#0,r6
		shll16	r6

		mov.l	SWIDTH,r3	! Calculate target position
		mul.l	r1,r3
		sts	macl,r3

		mov	r1,r5		! Calculate texture position
		and	r7,r5
		mov.l	TWIDTH,r8
		mul.l	r8,r5
		sts	macl,r5

		mov	r1,r11		! Calculate sine-distortion
		mov	r9,r12
		shll2	r12
		add	r12,r11
		lds	r11,fpul
		float	fpul,fr4
		mov	#50,r10
		lds	r10,fpul
		float	fpul,fr6

		fdiv	fr6,fr4
		mova	scale,r0
		fmov	@r0,fr0
		fmul	fr0,fr4
		ftrc	fr4,fpul
		fsca	fpul,dr0

		mova	FPOINT,r0
		fmov	@r0,fr2
		fmul	fr2,fr1
		fadd	fr2,fr1
		fadd	fr2,fr1

		ftrc	fr1,fpul
		sts	fpul,r10

		mov.l	WIDTH,r0
		shlr	r0
		lds	r0,fpul
		float	fpul,fr5
		fmul	fr1,fr5
		ftrc	fr5,fpul
		sts	fpul,r6
		neg	r6,r6

main_xloop:
		mov.l	VIDEO_RAM,r0
		mov.l	TEXTURE,r4

		mov	r6,r8
		shlr16	r8
		and	r7,r8
		add	r8,r4
		add	r5,r4

		add	r10,r6

		mov.b	@r4,r8
		mov.w	r8,@(r0,r3)
		add	#2,r3

		dt	r2
		bf	main_xloop

		dt	r1
		bf	main_yloop

		bra	main_begin
		nop

		.align	4
FPOINT:		.float	65536.0
CLRCOUNT:	.long	640*480/2
VIDEO_BASE:	.LONG	0xa5000000
TWIDTH:		.long	128
SWIDTH:		.long	640*2
WIDTH:		.long	320
HEIGHT:		.long	240
VIDEO_RAM:	.LONG	0xa5000000 + (640 - 320) + 240 * 640
TEXTURE:	.long	0x0cf00000
scale:		.float	10430.37835     ! 32768 / PI

		! "we still have 32 bytes left to fill and we are seriously running out of ideas" - .the .256b. ;P
		.space	32
