 org $8000

 ;1
 ldd #$ffff
 ldx #$0000
 ldy #$1111

 xgdx
 xgdy
 xgdx
;--------------------------------

 ;2
 ldx #$2222
 ldy #$3333

 stx $8201
 sty $8203
 ldx $8203
 ldy $8201
;--------------------------------

 ;3
 ldx #$4444
 ldy #$5555

 pshx
 pshy
 pulx
 puly

