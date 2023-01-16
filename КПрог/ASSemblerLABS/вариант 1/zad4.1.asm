 org $8000
 
 ldab #$02 ; 
 ldx  #ways   ;
p0 ldy 0,x ;
 jmp 0,y   ;


p1 nop ;
 inx ; 

 bra p0 ;
p2 bra p1 ; 
p3 brn * ; 
ways fdb p1,p2,p3