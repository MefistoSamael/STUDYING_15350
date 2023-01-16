 org $8000

 ldx #$8200

loop

 ldaa #8
 ldab 0,x
 mul 

 ldaa 0,x
 anda #%10111111
 staa 0,x

 lsra

 stab $8100
 anda $8100

 anda #%01000000
 oraa 0,x

 staa 0,x

 inx

 cmpx #$8300
 bne loop

