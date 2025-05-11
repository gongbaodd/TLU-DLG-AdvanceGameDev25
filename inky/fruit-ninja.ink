VAR speaker = "god"

Shhhh! This is the hell kitchen.
-> intro

=== intro ===
~ speaker = "god"
Do not weak up the host here.

* [What will happen if the owner finds out?] -> desp_host
+ [Okay] -> rat_come

=== desp_host ===
The owner will cast you out here!
+ [Okay] -> intro

=== rat_come ===
~ speaker = "boss"
Who is there? 
+ [It is me! Who are you?] -> desp_rat

=== desp_rat ===
~ speaker = "god"
This is a rat ghost. He must have one of the pieces. Ohhh, I can't stay for long. I will catch you when you get the piece.

+ [Okay] -> game_desp

=== game_desp ===
~ speaker = "boss"
Haha, you poor cat, want your master's memory piece? Come and catch it~ However, you can not make any sound to weak up the host.

+ [I will get my friend back!] -> Ending


=== Ending ====
The guy wakes up when all the soul pieces get back to him.

-> END
