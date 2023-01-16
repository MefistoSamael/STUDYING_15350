	org $8000

	ldaa #$ff
	ldx #$8200 ;начало массива
	ldab #$1 ;для инициализации
Init
	stab $0,x
	inx
	incb
	deca
	bne Init


	ldaa #$ff
	ldx #$8200
	ldab #$0 ;счетчик нечетных чисел

Zad
	brset 0,x,#%00000001,Inc  ;проверка, что число нечетное
	inx
	deca
	bne Zad  ;если а не ноль, то идем по циклу
	bsr End

Inc
	;если число нечетное, то сюда
	inx
	incb
	deca
	bne Zad

End