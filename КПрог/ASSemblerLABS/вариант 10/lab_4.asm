	org $8000

	ldaa #$ff
	ldx #$8200 ;������ �������
	ldab #$1 ;��� �������������
Init
	stab $0,x
	inx
	incb
	deca
	bne Init


	ldaa #$ff
	ldx #$8200
	ldab #$0 ;������� �������� �����

Zad
	brset 0,x,#%00000001,Inc  ;��������, ��� ����� ��������
	inx
	deca
	bne Zad  ;���� � �� ����, �� ���� �� �����
	bsr End

Inc
	;���� ����� ��������, �� ����
	inx
	incb
	deca
	bne Zad

End