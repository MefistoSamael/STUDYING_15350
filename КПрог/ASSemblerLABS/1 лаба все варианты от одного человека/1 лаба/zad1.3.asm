		org $8000
;position-independent implementation

 ldaa #$AA ; register initialization
 ldab #$55

 psha      ; push a & b to stack
 pshb
 pula      ; pop from stack
 pulb
 xgdx      ; copy x from d


 ;staa $8200  ; copy a to 8200
 ;tba         ; copy b to a
 ;ldab $8200  ; copy 8200 to b
 ;xgdx        ; copy x from d

 