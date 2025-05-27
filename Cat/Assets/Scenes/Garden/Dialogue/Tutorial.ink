VAR speaker = "god"

The Cat’s Awakening

-> START

=== START ===
(The world is quiet. A low hum echoes. You, a small but determined cat, awaken in your home's backyard.)

???: So... you’ve come after me.

* [Say nothing.]
    -> SILENT
* Who are you?
    -> ASK_NAME
* Where's my human?!
    -> ASK_HUMAN

=== SILENT ===
???: Hmph. Silent... but your eyes tell me everything. You're not giving up, are you?

-> CONTINUE1

=== ASK_NAME ===
???: Names? I’ve had many. But for now... you may call me the Ghost King.

-> CONTINUE1

=== ASK_HUMAN ===
???: That soul... it was glowing. Pure. I took it. I needed it. You wouldn’t understand.

-> CONTINUE1

=== CONTINUE1 ===
GHOST KING: You want them back? Then chase me. But you’ll have to collect every fragment of what was lost.

GHOST KING: Memories... scattered. Broken. Hiding in fruit. In enemies. In shadows.

* What do you mean?
    -> TUTORIAL1
* Give them back!
    -> THREATEN
* How do I find these memory pieces?
    -> TUTORIAL1

=== THREATEN ===
GHOST KING: Fierce little beast. But fury won’t save them. You’ll need precision... patience... claws.

-> TUTORIAL1

=== TUTORIAL1 ===
GHOST KING: In the realm ahead, objects will fly through the air.

GHOST KING: Slice them with your paws. Some hold memory pieces, others are just... fruit.

GHOST KING: Miss too many, and your chance may vanish.

-> TUTORIAL2

=== TUTORIAL2 ===
GHOST KING: Once you've collected enough, the gate will open. That’s when your claws will truly be tested.

GHOST KING: Enemies. Shadows. Regret.

* I’m ready.
    -> ending
* I’ll get them back... no matter what.
    -> ending
* This is your last warning.
    -> ending

=== ending ===
GHOST KING: Then come, little flame. Let’s see how far your devotion burns...

(The ghost fades into mist. The wind picks up. The fruit realm awakens.)

-> END
