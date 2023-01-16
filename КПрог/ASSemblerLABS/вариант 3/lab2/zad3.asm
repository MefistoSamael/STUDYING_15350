		org $8000

 ldaa #$54
 ldab #$40
 ldx #$0420
 ldy #$0020

 stx $8100
 sty $8102

 SUBD $8100
 SUBD $8102