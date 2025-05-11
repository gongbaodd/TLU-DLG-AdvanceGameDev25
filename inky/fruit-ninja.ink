VAR speaker = "god"
VAR isGaming = false
VAR isWon = false

Shhhh! This is the hell kitchen.
+ [Okay] -> intro

=== intro ===
~ speaker = "god"
Do not weak up the host here.

* [The host?] -> desp_host
+ [Okay] -> rat_come

=== desp_host ===
The host will cast you out here!
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
Haha, you poor cat, want your master's memory piece? Come and catch it~ Whatever, you can not make any sound to weak up the host.

+ [I will get my friend back!] -> game

=== game ===
~ isGaming = true

+ [Win] -> Win_game
+ [Fail] -> Lose_game

=== Lose_game ===
~ isGaming = false
~ speaker = "boss"
You are so clumsy.

+ [I have to leave.] -> Ending

=== Win_game ===
~ isGaming = false
~ speaker = "boss"
~ isWon = true
Noooo! I'd better run before master gets home.

+ [Wait! Your master is not home?] -> Ending


=== Ending ====
~ speaker = "god"
{isWon:
    The host is not home! It is so good! Let's go back to save your friend first.
- else:
    It is fine, we can always come back.
}

+ [Okay] -> END
