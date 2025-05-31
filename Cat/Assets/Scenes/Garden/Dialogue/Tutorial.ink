VAR speaker = "god"
VAR name_given = 0
VAR is_fruit_ninja_done = false
VAR is_diablo_done = false
VAR is_goto_fruit_ninja = false
VAR is_goto_diablo = false


You, a small but determined cat, awaken in your home's backyard.

+ [Continue] -> START

=== START ===
~ speaker = "god"
{is_fruit_ninja_done && !is_diablo_done: -> TUTORIAL2}
{is_fruit_ninja_done && is_diablo_done: -> ending}
???: So... you’ve come after me.

* [Say nothing.] -> SILENT
* [Who are you?] -> ASK_NAME

=== SILENT ===
???: Hmph. Silent... but your eyes tell me everything. You're not giving up, are you? 
+[Continue]-> CONTINUE1

=== ASK_NAME ===
~ name_given += 1  
???: Names? I’ve had many. But for now... you may call me the Ghost King.
+[Continue]-> CONTINUE1

=== CONTINUE1 ===
{ name_given > 0:
GHOST KING: You want your owner back? Then chase after me. But you’ll have to collect every fragment of what was lost.
- else:
???: How boring! If you want your owner back, then chase after me. But you’ll have to collect every fragment of what was lost.
}
* [What do you mean?] -> TUTORIAL1
* [Give them back!] -> THREATEN

=== THREATEN ===
{ name_given > 0:
    GHOST KING: Fierce little beast. But fury won’t save him. You’ll need precision... patience... claws.
- else:
    ???: Fierce little beast. But fury won’t save him. You’ll need precision... patience... claws.
}

+[Continue]-> TUTORIAL1

=== TUTORIAL1 ===
{ name_given > 0:
    GHOST KING: In the realm ahead, objects will fly through the air. Slice them with your paws. Some hold memory pieces, others are just... fruit.
- else:
    ???: In the realm ahead, objects will fly through the air. Slice them with your paws. Some hold memory pieces, others are just... fruit.
}
 
+[Continue]-> GOTO_FRUIT_NINJA

=== GOTO_FRUIT_NINJA ===
~ is_goto_fruit_ninja = true
-> DONE

=== TUTORIAL2 ===
{ name_given > 0:
    GHOST KING: Once you've collected enough, the gate will open. That’s when your claws will truly be tested.
- else:
    ???: Once you've collected enough, the gate will open. That’s when your claws will truly be tested.
}

* [I’m ready.] -> GOTO_DIABLO
* [You'll regret this!] -> GOTO_DIABLO

=== GOTO_DIABLO ===
~ is_goto_diablo = true
-> DONE

=== ending ===
~ speaker = "god"
{ name_given > 0:
    GHOST KING: Then come, little flame. Let’s see how far your devotion burns...  
- else:
    ???: Then come, little flame. Let’s see how far your devotion burns...
}

+ [Continue] -> END
