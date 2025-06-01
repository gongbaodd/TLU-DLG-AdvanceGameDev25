VAR isGaming = false
VAR speaker = "god"

(The shadows are thicker here. The air smells of rust and secrets. You've made it — to the heart of the Ghost King's lair.)

+ [Continue] -> intro

=== intro ===
~ speaker = "god"
GHOST KING: You clawed your way through every illusion, every minion... and now you stand before my vault?

+ [Give him back.] -> monsters

=== monsters ===
GHOST KING: You’re bold, little flame. But not everything here is what it seems. Some chests hold truth. Others — teeth.

+ [I’m not afraid of your tricks.] -> game

=== game ===
~ isGaming = true
(The final trial. The stolen memories whisper from within the chests. But the Ghost King has twisted them — only one holds what you seek.)

+ [I found it.] -> Win_game
+ [I failed…] -> Lose_game

=== Win_game ===
~ isGaming = false
~ speaker = "god"
GHOST KING: Impossible... You’ve undone what I shattered.

(The final memory glows in your paws. It hums with warmth — a promise of return.)

+ [I’m taking him back.] -> END

=== Lose_game ===
~ isGaming = false
~ speaker = "god"
GHOST KING: Lost, are you? No matter. I’ll scatter the pieces again if I must.

(The chests vanish in smoke. But somewhere in the dark, the final memory still calls to you.)

+ [I won’t give up.] -> END
