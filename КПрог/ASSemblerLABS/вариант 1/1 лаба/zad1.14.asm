 org $8000

 ldaa #$aa
 ldab #$bb
 ldx  #$cccc
 ldy  #$dddd


 psha
 pshb
 pshx
 pshy

 tpa
 ;psha
 
 staa $8100

 pulx
 stx  $8101

 pulx
 stx  $8103

 pulx
 stx  $8105

 psha
 tsx

 stx  $8107




