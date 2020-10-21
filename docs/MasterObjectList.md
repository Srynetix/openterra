# Master Object List (extracted from SubTerra I)

The list is divided into the following sections, in that order: *Walls*, *Player passages*, *Floor tiles*, *Tools*, *Movable objects*, *Falling objects*, *Triggers*, *Enemies*, and *Miscellaneous*.  
Unless noted otherwise, any object can be removed by a laser (L) or explosion <img src="./tiles/explosion.png" width="16" />.
Note that there are no such things as illusory objects in the game; everything is exactly what it appears to be. No invisible walls, no secret passages, etc.  
Nor are there random objects in the game. Note that the skelwing and poltergeist are not in fact random; they behave the same way every time.

## **Walls**

All walls block passage for everything.

### ![](./tiles/wall.png) **Wall**
No further remarks, it's just a wall.

### ![](./tiles/trwall.png) ![](./tiles/tlrwall.png) ![](./tiles/trrwall.png) **Rounded wall**
Falling objects can roll off in the designated direction(s).  
If an object can roll both to the left and to the right, it will roll to the left.

### ![](./tiles/glasswall.png) **Glass wall**
Lasers will pass through rather than destroying it.

### ![](./tiles/gemwall.png) ![](./tiles/cratewall.png) ![](./tiles/balloonwall.png) ![](./tiles/keywall.png) **Wall with embedded object**
The embedded object is released when the wall is destroyed by explosion <img src="./tiles/explosion.png" width="16" />, laser (L) or star trigger <img src="./tiles/strigger.png" width="16" />.  
See below for descriptions of the objects inside the wall.

### ![](./tiles/steelwall.png) **Steel wall**
Not affected by explosions <img src="./tiles/explosion.png" width="16" /> and lasers (L).  
Note that the border of the level is considered steel wall even if it's usually out of sight.

### ![](./tiles/hexwall.png) ![](./tiles/vexwall.png) ![](./tiles/fexwall.png) **Extending wall**
These expands in the designated directions.  
They may be destroyed but if some part remains it will immediately start expanding again.  
The activator <img src="./tiles/activator.png" width="16" /> removes all extending walls within its range.  
Extending wall moves at half speed.  
Is affected by traps <img src="./tiles/trap.png" width="16" />; other floor tiles are removed as the wall extends.

### ![](./tiles/abbarrier.png) ![](./tiles/dbbarrier.png) ![](./tiles/apbarrier.png) ![](./tiles/dpbarrier.png) **Barriers**
A barrier may only be passed by any object when it's disabled.  
An active barrier is not affected by explosions <img src="./tiles/explosion.png" width="16" /> and lasers (L).  
A disabled barrier is not affected by lasers (L) but may be removed by explosions <img src="./tiles/explosion.png" width="16" />.  
The switch <img src="./tiles/bbswitch.png" width="16" /> <img src="./tiles/pbswitch.png" width="16" /> toggles all barriers of the appropriate color.  
The activator <img src="./tiles/activator.png" width="16" /> also toggles all barriers within its radius.  
If an object is over a disabled barrier, it will be destroyed when the barrier is switched on.

### ![](./tiles/spike.png) **Spike**
Falling objects can roll off in either direction.  
When a balloon <img src="./tiles/balloon.png" width="16" /> moves upward into a spike, it pops and disappears but the spike does not.  
If a balloon pushes a bomb <img src="./tiles/bomb.png" width="16" /> or mine <img src="./tiles/mine.png" width="16" /> into it, it will cause the bomb or mine to explode.

### ![](./tiles/qsand.png) **Quicksand**
Rocks <img src="./tiles/rock.png" width="16" /> and keys <img src="./tiles/bkey.png" width="16" /> can pass through quicksand at half speed.  
Objects won't fall out of quicksand if anything is underneath, and won't harm the object underneath either.  
Objects cannot be pushed into or out of quicksand.  
Some quicksand starts with a rock <img src="./tiles/rock.png" width="16" /> embedded in it.

### ![](./tiles/fwall.png) ![](./tiles/iwall.png) **Fragile walls**
When it by a rock <img src="./tiles/rock.png" width="16" />, quantum stone <img src="./tiles/qstone.png" width="16" />, orb <img src="./tiles/orb.png" width="16" /> or bomb <img src="./tiles/bomb.png" width="16" />, it breaks and the object falls through.  
If the blue wall <img src="./tiles/iwall.png" width="16" /> is hit in this way, it changes to ice <img src="./tiles/ice.png" width="16" />.  
A chain of these can be removed by a star trigger <img src="./tiles/strigger.png" width="16" />.  
These can be created by moving ice blocks <img src="./tiles/iblock.png" width="16" /> onto ice <img src="./tiles/ice.png" width="16" />.

### ![](./tiles/bcbelt.png) ![](./tiles/rcbelt.png) ![](./tiles/ycbelt.png) ![](./tiles/gcbelt.png) **Conveyor belt**
When turned off, falling objects can roll off in either direction.  
When turned on, falling objects will roll in its indicated direction.  
Balloons <img src="./tiles/balloon.png" width="16" />, when directly underneath, will move in the direction opposite to the conveyor's.  
Red, green and yellow conveyors can be turned on or off by the appropriate switches <img src="./tiles/rcswitch.png" width="16" /> <img src="./tiles/ycswitch.png" width="16" /> <img src="./tiles/gcswitch.png" width="16" />.  
The blue <img src="./tiles/bcbelt.png" width="16" /> conveyor belt is always on. Two varieties exist, one moves to the left, the other to the right.

### ![](./tiles/dup.png) **Duplicator**
When a falling object falls onto a duplicator, two versions of that same object will roll out to either side.  
If one side is blocked, only the other side yields an object. If both sides are blocked, the object simply disappears.  
Eggs <img src="./tiles/egg.png" width="16" /> and bombs <img src="./tiles/bomb.png" width="16" /> cannot be duplicated (bombs explode, of course).

### ![](./tiles/brtrans.png) **Brown transmuter**
When a falling object falls onto a transmuter, it moves through and changes to something else.  
If the tile below a transmuter is blocked, the object disappears instead.  
A rock <img src="./tiles/rock.png" width="16" /> or mine <img src="./tiles/mine.png" width="16" /> changes to a diamond <img src="./tiles/diamond.png" width="16" />.  
A diamond <img src="./tiles/diamond.png" width="16" /> or quantum stone <img src="./tiles/qstone.png" width="16" /> changes to an emerald <img src="./tiles/emerald.png" width="16" />.  
An emerald <img src="./tiles/emerald.png" width="16" /> changes to a rock <img src="./tiles/rock.png" width="16" />.  
A ruby <img src="./tiles/ruby.png" width="16" /> changes to a red key <img src="./tiles/rkey.png" width="16" />.  
An egg <img src="./tiles/egg.png" width="16" /> or orb <img src="./tiles/orb.png" width="16" /> will not fall through a transmuter.  
A bomb <img src="./tiles/bomb.png" width="16" /> will explode when it hits the transmuter.  
A key will change color: red <img src="./tiles/rkey.png" width="16" /> to yellow <img src="./tiles/ykey.png" width="16" />, to green <img src="./tiles/gkey.png" width="16" />, to blue <img src="./tiles/bkey.png" width="16" />, to red <img src="./tiles/rkey.png" width="16" />.

### ![](./tiles/bltrans.png) **Blue transmuter**
Behaves exactly the same as a regular transmuter except for the following:  
A diamond <img src="./tiles/diamond.png" width="16" /> changes to a rock <img src="./tiles/rock.png" width="16" />.  
An emerald <img src="./tiles/emerald.png" width="16" /> changes to a bomb <img src="./tiles/bomb.png" width="16" />.  
A key will change color: red <img src="./tiles/rkey.png" width="16" /> to blue <img src="./tiles/bkey.png" width="16" />, to green <img src="./tiles/gkey.png" width="16" />, to yellow <img src="./tiles/ykey.png" width="16" />, to red <img src="./tiles/rkey.png" width="16" />.  
An egg <img src="./tiles/egg.png" width="16" /> hatches to a cryo bird <img src="./tiles/cryo.png" width="16" />.

## **Player passages**

These may only be passed by players <img src="./tiles/player.png" width="16" />. Other objects treat these as walls.

### ![](./tiles/marble.png) **Marble**
Not affected by explosions <img src="./tiles/explosion.png" width="16" />; however, can be removed by lasers (L).  
A player <img src="./tiles/player.png" width="16" /> cannot be harmed in any way while standing on marble, except if the marble is removed by the laser.

### ![](./tiles/dirt.png) ![](./tiles/gdirt.png) **Dirt**
Turns into empty space when a player <img src="./tiles/player.png" width="16" /> passes through.  
Some dirt contains small diamonds, these are worth two points towards the gem quota. This increases your score by twice the value of a diamond <img src="./tiles/diamond.png" width="16" />.  
Flowstone <img src="./tiles/fstone.png" width="16" /> ignores dirt.

### ![](./tiles/passage.png) **Small passage**
Turns into a wall when a player <img src="./tiles/player.png" width="16" /> passes through.  
The activator <img src="./tiles/activator.png" width="16" /> changes all small passages in its range into walls.  
If the player <img src="./tiles/player.png" width="16" /> is on the passage when this happens, the player is not harmed.

### ![](./tiles/rdoor.png) ![](./tiles/ydoor.png) ![](./tiles/gdoor.png) ![](./tiles/bdoor.png) **Doors**
Doors are not affected by explosions <img src="./tiles/explosion.png" width="16" /> but can be removed by lasers (L).  
A player <img src="./tiles/player.png" width="16" /> can pass through a door only while carrying the appropriate key <img src="./tiles/gkey.png" width="16" />.  
This removes both key and door.

### ![](./tiles/rgate.png) ![](./tiles/ygate.png) ![](./tiles/ggate.png) ![](./tiles/bgate.png) **Gates**
The player <img src="./tiles/player.png" width="16" /> can pass through a gate only while carrying the appropriate key <img src="./tiles/rkey.png" width="16" />.  
This removes neither key nor gate.  
While passing through a gate the player moves at double speed.  
Note that a gate cannot be passed if anything is behind it.

### ![](./tiles/dport.png) ![](./tiles/rport.png) ![](./tiles/vport.png) ![](./tiles/fport.png) **Ports**
Ports may be passed only in the designated direction(s).  
While passing through a port the player <img src="./tiles/player.png" width="16" /> moves at double speed.  
Seekers <img src="./tiles/seek.png" width="16" /> ignore ports and may pass them in any direction.  
Note that a port cannot be passed if anything is behind it, including another port.  

### ![](./tiles/ncexit.png) ![](./tiles/hcexit.png) **Exit**
To complete the level, all players <img src="./tiles/player.png" width="16" /> must exit the level and no player may have died.  
All exits open <img src="./tiles/noexit.png" width="16" /> <img src="./tiles/hoexit.png" width="16" /> when the players have collected enough gems.  
The blue exit is unavailable <img src="./tiles/ncexit.png" width="16" /> in hard mode.  
The red exit turns into a wall after one player passes through.  
It is rumored that there's also a third kind of exit - a secret exit...  
Note that the hard gem quota will be ignored if it is less than the easy gem quota.

## **Floor tiles**

Lasers (L) ignore (that is, pass through) all floor tiles.  
Extending walls <img src="./tiles/vexwall.png" width="16" /> and flowstone <img src="./tiles/fstone.png" width="16" /> ignore all floor tiles except for traps <img src="./tiles/trap.png" width="16" />.  
Unless otherwise specified, all objects move over floor tiles normally.  
Floor tiles are the only objects that cannot be cloned <img src="./tiles/lclone.png" width="16" />

### ![](./tiles/ice.png) **Ice**
Objects entering ice keep moving in the same direction if possible.  
If there is more ice, they keep moving until no longer on ice.  
If that direction is blocked, they move normally.  
Players <img src="./tiles/player.png" width="16" /> ignore ice if they carry skates <img src="./tiles/skates.png" width="16" />.  
When an explosive <img src="./tiles/explosive.png" width="16" /> moves onto ice, it disappears and so does the ice.  
When an ice block <img src="./tiles/iblock.png" width="16" /> moves onto ice, it disappears and adds a fragile wall <img src="./tiles/iwall.png" width="16" /> onto the ice.  
Certain buttons <img src="./tiles/bbswitch.png" width="16" /> also cause objects to slide over them.

### ![](./tiles/fire.png) **Fire**
Players <img src="./tiles/player.png" width="16" /> may not pass fire unless they carry an extinguisher <img src="./tiles/extinguisher.png" width="16" />.  
Bombs <img src="./tiles/bomb.png" width="16" /> and mines <img src="./tiles/mine.png" width="16" /> explode when entering fire.  
Other falling objects, and firedrakes <img src="./tiles/firedrake.png" width="16" />, ignore fire.  
Other creatures die (but do not explode) when they enter fire.  
When an ice block <img src="./tiles/iblock.png" width="16" /> enters fire, it disappears and so does the fire.  
When a pillow <img src="./tiles/pillow.png" width="16" /> enters fire, it disappears but the fire does not.  
When an explosive <img src="./tiles/explosive.png" width="16" /> enters fire, it causes a small explosion.  
This explosion leaves a total of five fires behind.  
When a balloon <img src="./tiles/balloon.png" width="16" /> enters fire, it causes a small explosion <img src="./tiles/explosion.png" width="16" />.

### ![](./tiles/water.png) **Water**
Players <img src="./tiles/player.png" width="16" /> may not pass water unless they carry a life belt <img src="./tiles/lifebelt.png" width="16" />.  
Cryo birds <img src="./tiles/cryo.png" width="16" /> and ice blocks <img src="./tiles/iblock.png" width="16" /> ignore water.  
All other creatures, and falling objects, are removed when entering water; they will not explode.  
When a crate <img src="./tiles/crate.png" width="16" /> enters water, it disappears and the water changes to ice <img src="./tiles/ice.png" width="16" />.  
When a safe <img src="./tiles/safe.png" width="16" /> enters water, it disappears and the water changes to dirt <img src="./tiles/dirt.png" width="16" />.  
When a pillow <img src="./tiles/pillow.png" width="16" /> enters water, it disappears and the water changes to a raft <img src="./tiles/raft.png" width="16" />.  
When an explosive <img src="./tiles/explosive.png" width="16" /> enters water, it disappears but the water does not.  

### ![](./tiles/raft.png) **Raft**
When a player <img src="./tiles/player.png" width="16" /> steps onto a raft, he keeps moving in the same direction just as he would on ice <img src="./tiles/ice.png" width="16" />.  
If the next tile is water <img src="./tiles/water.png" width="16" />, the raft is moved one square along with the player.  
If the next tile is another raft, the player moves onto that raft and continues along the water.  
The effect is that both can cross any amount of water. The player gets off at the other side.

### ![](./tiles/umfield.png) ![](./tiles/dmfield.png) ![](./tiles/lmfield.png) ![](./tiles/rmfield.png) **Motion fields**
Objects entering a motion field are moved in the direction indicated by the field.  
If that direction is blocked, they may move normally.
Directional crates <img src="./tiles/hcrate.png" width="16" /> can't move sideways to their arrow, not event on a motion field.  
Players <img src="./tiles/player.png" width="16" /> ignore motion fields if they carry suction shoes <img src="./tiles/shoes.png" width="16" />.  
Players can ignore each second square of motion field as long as they keep moving.

### ![](./tiles/selector.png) **Selector**
When a player <img src="./tiles/player.png" width="16" /> moves out of a selector field, it changes to become a motion field <img src="./tiles/umfield.png" width="16" /> pointing in whatever direction the player was moving.  
The same applies to slimeys <img src="./tiles/slimey.png" width="16" />, spiders <img src="./tiles/spider.png" width="16" />, cryo birds <img src="./tiles/cryo.png" width="16" /> and firedrakes <img src="./tiles/firedrake.png" width="16" />.  
Other objects ignore selectors.

### ![](./tiles/ccwrfield.png) ![](./tiles/cwrfield.png) **Rotation fields**
Objects entering a rotation field turn ninety degrees then move out.  
For instance if a creature enters the clockwise field from the top, it will leave to the right.  
If that direction is blocked, they may move normally.  
When a directional crate <img src="./tiles/hcrate.png" width="16" /> enters a rotation field, it rotates ninety degrees.

### ![](./tiles/bdfield.png) ![](./tiles/sdfield.png) **Deflection fields**
Objects entering a deflection field bounce in a ninety degree angle, then move out.  
This happens even to directional crates <img src="./tiles/vcrate.png" width="16" />.  
For instance if a player <img src="./tiles/player.png" width="16" /> enters the backslash field from the bottom, he will leave to the left.  
If that direction is blocked, they may move normally.

### ![](./tiles/glue.png) **Glue**
Players <img src="./tiles/player.png" width="16" /> move at half speed when moving out of glue, or from one glue tile to another.  
Balloons <img src="./tiles/balloon.png" width="16" /> ignore glue.  
When a slimey <img src="./tiles/slimey.png" width="16" /> enters glue, it disappears and the glue changes to dirt with small gems <img src="./tiles/gdirt.png" width="16" />.  
All other objects are held fast by glue.  
Enemies held in glue will not explode to kill adjacent players <img src="./tiles/player.png" width="16" /> or flowstone <img src="./tiles/fstone.png" width="16" />.  
A ruby <img src="./tiles/ruby.png" width="16" /> stuck in glue will not trigger a laser beam if adjacent to a skull <img src="./tiles/skull.png" width="16" />.  
Falling objects held in glue can be dislodged by pushing them downward, or by dropping a rock <img src="./tiles/rock.png" width="16" />, quantum stone <img src="./tiles/qstone.png" width="16" /> or orb <img src="./tiles/orb.png" width="16" /> on them.

### ![](./tiles/trap.png) **Trap**
Any object entering a trap is held fast.  
When the appropriate release button <img src="./tiles/rtrap.png" width="16" /> is triggered, the object is released and moves out normally.  
A release button opens the first trap to the right of it.  
If no traps are to the button's direct right, the line below is checked and so forth.  
The activator <img src="./tiles/activator.png" width="16" /> also opens all traps within its radius.  
If the object stuck in the trap doesn't move out immediately, the trap closes again.  
Enemies held in traps will not explode to kill adjacent players <img src="./tiles/player.png" width="16" /> or flowstone <img src="./tiles/fstone.png" width="16" />.  
Falling objects held in traps can be dislodged by pushing them downward (or sideways for that matter).

### ![](./tiles/warp.png) **Teleporter**
When an object enters a teleporter, it will exit from the next teleporter moving in the same direction.  
The 'next teleporter' means the first one to the right of the current one.  
If there are not teleporters to the direct right, the line below is checked and so forth.  
If that is blocked, it checks the next teleporter and tries to exit there.  
If all other teleporters are blocked, the object exists from the original teleporter.  
If that direction, too, is blocked, the object leaves the original teleporter the way it came in.  
If that, too, is blocked, the object is destroyed.  
This will cause bombs <img src="./tiles/bomb.png" width="16" />, mines <img src="./tiles/mine.png" width="16" />, explosives <img src="./tiles/explosive.png" width="16" />, enemies and players <img src="./tiles/player.png" width="16" /> to explode.

### ![](./tiles/toll.png) **Toll sign**
A player <img src="./tiles/player.png" width="16" /> may pass the toll sign but this increates the gem quota by one.  
Doing this too often may leave the level unsolvable.  
When any other object enters a toll sign, it moves back in the direction it came from.  
If that is blocked, the object moves normally.  
When the exit <img src="./tiles/ncexit.png" width="16" /> <img src="./tiles/noexit.png" width="16" /> opens, all toll signs disappear.  
Note that even in hard mode, the toll signs disappear when the easy exit would have opened.

#### ![](./tiles/munch.png) **Munchkin**
When a player <img src="./tiles/player.png" width="16" /> steps onto a munchkin, he loses all of the following that he carries: Shield <img src="./tiles/shield.png" width="16" />, extinguisher <img src="./tiles/extinguisher.png" width="16" />, life belt <img src="./tiles/lifebelt.png" width="16" />, suction shoes <img src="./tiles/shoes.png" width="16" />, skates <img src="./tiles/skates.png" width="16" /> and magnet <img src="./tiles/magnet.png" width="16" />.  
Other objects ignore munchkins.

## **Tools**

A player <img src="./tiles/player.png" width="16" /> may move through a tool (or grab it from a distance), this picking it up.  
A player may pick up multiples of the same tool, but these have no additional effect.  
A player loses all tools when stepping onto a munchkin <img src="./tiles/munch.png" width="16" />, except for dynamite <img src="./tiles/dynamite.png" width="16" />.  
Cryo birds <img src="./tiles/cryo.png" width="16" /> may also move through tools, this picking them up.  
Flowstone <img src="./tiles/fstone.png" width="16" /> ignores tools and moves over them.  
All other objects treat tools as if they were walls.

### ![](./tiles/shoes.png) **Suction shoes**
A player <img src="./tiles/player.png" width="16" /> carrying suction shoes ignores motion fields <img src="./tiles/dmfield.png" width="16" />, but not rotation or deflection fields.

### ![](./tiles/lifebelt.png) **Life belt**
A player carrying a life belt ignores water <img src="./tiles/water.png" width="16" />.

### ![](./tiles/extinguisher.png) **Extinguisher**
A player carrying an extinguisher ignores fire <img src="./tiles/fire.png" width="16" />.

### ![](./tiles/skates.png) **Skates**
A player carrying skates ignores ice <img src="./tiles/ice.png" width="16" />.

### ![](./tiles/shield.png) **Shield**
A player carrying a shield will not cause adjacent enemies to explode.

### ![](./tiles/magnet.png) **Magnet**
A player carrying a magnet may pull any object that can normally be pushed - including gems <img src="./tiles/diamond.png" width="16" /> <img src="./tiles/emerald.png" width="16" />.

### ![](./tiles/dynamite.png) **Dynamite**
Explodes when hit by an explosion <img src="./tiles/explosion.png" width="16" />, laser (L) or activator <img src="./tiles/activator.png" width="16" />.  
Can be dropped by the player <img src="./tiles/player.png" width="16" /> which causes it t explode after three seconds.  
To drop a bar of dynamite, hold control and press any direction to run away.  
Flowstone <img src="./tiles/fstone.png" width="16" /> does not grow over dynamite.  
Is not removed by the munchkin <img src="./tiles/munch.png" width="16" />.

## Movable objects

All these can be pushed by players <img src="./tiles/player.png" width="16" />, but only one can be pushed at a time.  
A player carrying a magnet <img src="./tiles/magnet.png" width="16" /> may also pull these objects.

### ![](./tiles/crate.png) **Crate**
Can be pushed around by movers <img src="./tiles/mover.png" width="16" /> as well.  
Players <img src="./tiles/player.png" width="16" /> as well as movers <img src="./tiles/mover.png" width="16" /> can push multiple crates in a row, as long as no other objects are in the way.  
Keys <img src="./tiles/rkey.png" width="16" /> can be pushed in the same way as crates, so a row of crates and keys can be pushed.  
When moved into fire <img src="./tiles/fire.png" width="16" />, it disappears but the fire does not.  
When moved into water <img src="./tiles/water.png" width="16" />, it disappears and changes to water to ice <img src="./tiles/ice.png" width="16" />.  
A chain of these can be removed by a star trigger <img src="./tiles/strigger.png" width="16" />

### ![](./tiles/hcrate.png) ![](./tiles/vcrate.png) **Directional crate**
Works just like a crate, except that it can only move in the indicated directions.  
This also means it can only be cloned <img src="./tiles/dclone.png" width="16" /> in the indicated directions.  
This means that even on a motion field <img src="./tiles/dmfield.png" width="16" />, the crate can't move sideways to its arrow.  
When it enters a rotation field <img src="./tiles/ccwrfield.png" width="16" />, it rotates, thus changing into the other kind of directional crate.  
Deflection fields <img src="./tiles/bdfield.png" width="16" /> move a directional crate just like any other object.

### ![](./tiles/pillow.png) **Pillow**
Bombs <img src="./tiles/bomb.png" width="16" /> and mines <img src="./tiles/mine.png" width="16" /> will not explode when landing on a pillow.  
When moved into fire <img src="./tiles/fire.png" width="16" />, it disappears but the fire does not.  
When moved into water <img src="./tiles/water.png" width="16" />, it disappears and changes the water to a raft <img src="./tiles/raft.png" width="16" />.  
Falling objects can roll off of pillows.

### ![](./tiles/safe.png) **Safe**
When hit by an explosion <img src="./tiles/explosion.png" width="16" /> or laser (L), it changes into an egg <img src="./tiles/egg.png" width="16" />.  
When moved into water <img src="./tiles/water.png" width="16" />, it disappears and changes the water to dirt <img src="./tiles/dirt.png" width="16" />.  
Ignores fire <img src="./tiles/fire.png" width="16" />.

### ![](./tiles/iblock.png) **Ice block**
When pushed, it will keep moving at half speed until it hits something.  
Since it moves at half speed the player <img src="./tiles/player.png" width="16" /> can run around it and halt it again.  
Not affected by explosions <img src="./tiles/explosion.png" width="16" /> and lasers (L).  
Ignores water <img src="./tiles/water.png" width="16" />.
When moved into fire <img src="./tiles/fire.png" width="16" />, it disappears and so does the fire.  
When moved into ice <img src="./tiles/ice.png" width="16" />, it disappears and adds a fragile wall <img src="./tiles/iwall.png" width="16" /> onto the ice.  
When hit by a rock <img src="./tiles/rock.png" width="16" />, quantum stone <img src="./tiles/qstone.png" width="16" />, orb <img src="./tiles/orb.png" width="16" /> or bomb <img src="./tiles/bomb.png" width="16" />, it shatters.  
This even happens when hit in the side by an orb <img src="./tiles/orb.png" width="16" />.  
Falling objects can roll off of ice blocks.

### ![](./tiles/explosive.png) **Explosive**
When hit by an explosion <img src="./tiles/explosion.png" width="16" /> or laser (L), it explodes.  
When hit on top by any falling object, it also explodes.  
When moved onto ice <img src="./tiles/ice.png" width="16" />, it disappears and so does the ice.  
When moved onto fire <img src="./tiles/fire.png" width="16" />, it causes a small explosion. This explosion leaves a total of five fires behind.  
When moved into water <img src="./tiles/water.png" width="16" />, it disappears but the water does not.  
Triggering the detonator <img src="./tiles/detonator.png" width="16" /> causes all explosives in the level to explode.  
The activator <img src="./tiles/activator.png" width="16" /> sets off all explosives within its range.  
Falling objects can roll off of explosives.

### ![](./tiles/mover.png) **Mover**
Cannot be pushed or pulled.  
Starts either moving in a certain direction, or stationary.  
When moving, it keeps moving in that direction until it hits something, then it stops.  
If whatever was in front of it is removed, it will start moving again.  
Note that if it was pushing something and was blocked, and the object blocking the push is removed, it will not start pushing again of its own accord.  
When it moves into water <img src="./tiles/water.png" width="16" />, it disappears but the water does not.  
Falling objects can roll off of movers, but only if the mover is not currently moving.  
Several switches <img src="./tiles/lmswitch.png" width="16" /> <img src="./tiles/fmswitch.png" width="16" /> <img src="./tiles/smswitch.png" width="16" /> exist that cause all movers to move in a certain direction.

### ![](./tiles/elevator.png) **Elevator**
Cannot be pushed or pulled.  
Moves upward until it hits something, then downward until it hits something, then back up, etc.  
Can push one falling object upward. The object will only roll off if it can no longer move upward.  
When it moves into water <img src="./tiles/water.png" width="16" />, it disappears but the water does not.  
If it hits a creature while moving downward, causes that creature to explode.  
If it hits an egg <img src="./tiles/egg.png" width="16" /> while moving downward, the egg opens, revealing a diamond <img src="./tiles/diamond.png" width="16" />.

### ![](./tiles/balloon.png) **Balloon**
Can only be pushed or pulled to the left and to the right.  
Moves upward whenever possible.  
Can push one falling object upward. The object will only roll off if it can no longer move upward.  
When directly under two falling objects, the balloon will move down.  
When hit by an explosion <img src="./tiles/explosion.png" width="16" /> or laser (L), it causes a small explosion.  
Ignores glue <img src="./tiles/glue.png" width="16" />.  
When moved onto fire <img src="./tiles/fire.png" width="16" />, it causes a small explosion <img src="./tiles/explosion.png" width="16" />.  
The activator <img src="./tiles/activator.png" width="16" /> causes all balloons within range to explode.  
When moved into water <img src="./tiles/water.png" width="16" />, it disappears but the water does not.  
When moving upward into a spike <img src="./tiles/spike.png" width="16" />, it disappears but the spike does not.  
If pushing a bomb <img src="./tiles/bomb.png" width="16" /> or mine <img src="./tiles/mine.png" width="16" /> into a spike <img src="./tiles/spike.png" width="16" />, it will cause the bomb or mine to explode.  
When directly under a conveyor <img src="./tiles/ycbelt.png" width="16" />, it will move in the direction opposite to the conveyor's.

## Falling objects

All falling objects move down whenever possible.  
Falling objects will roll off one another. This means that if one rock is on top of another and both are stationary, the top rock will roll to the side if the tile diagonally down is free.  
If an object can roll both to the left and to the right, il will roll to the left.  
Falling objects may also roll of the following: pillow <img src="./tiles/pillow.png" width="16" />, ice block <img src="./tiles/iblock.png" width="16" />, explosive <img src="./tiles/explosive.png" width="16" />, rounded wall <img src="./tiles/trwall.png" width="16" />, conveyor <img src="./tiles/rcbelt.png" width="16" />, skull <img src="./tiles/skull.png" width="16" /> and spike <img src="./tiles/spike.png" width="16" />.  
All falling objects can be pushed by players <img src="./tiles/player.png" width="16" />, but only one can be pushed at a time and they can only be pushed to the left and to the right.  
A player carrying a magnet <img src="./tiles/magnet.png" width="16" /> may also pull these objects, again only to the left and to the right. Yes, you can pull diamonds <img src="./tiles/diamond.png" width="16" />.  
Gems cannot be pushed, rather thet are picked up by the player. They may, however, be pulled.  
Keys <img src="./tiles/ykey.png" width="16" /> can only be moved if the player already carries a key of that color, they are picked up otherwise.  
If a falling object is stuck in glue <img src="./tiles/glue.png" width="16" /> or a trap <img src="./tiles/trap.png" width="16" />, it can be pushed down.  
Falling objects held in glue <img src="./tiles/glue.png" width="16" /> can be dislodged by droiing a rock <img src="./tiles/rock.png" width="16" />, quantum stone <img src="./tiles/qstone.png" width="16" /> or orb <img src="./tiles/orb.png" width="16" /> on them.  
Falling objects ignore fire <img src="./tiles/fire.png" width="16" />, with the exception of bombs <img src="./tiles/bomb.png" width="16" /> and mines <img src="./tiles/mine.png" width="16" />, which explode instead.  
When a falling object enters water <img src="./tiles/water.png" width="16" />, it disappears but the water does not.
When it falls onto a transmuter <img src="./tiles/brtrans.png" width="16" />, a falling objects moves through and changes to something else.  
If the tile velow a transmuter is blocked, it disappears instead.  
When it falls onto a duplicator <img src="./tiles/dup.png" width="16" />, two versions of that same object will roll out to either side, unless blocked.  
Note that the object must fall onto a transmuter or duplicator for it to be effective.  
Firedrakes <img src="./tiles/firedrake.png" width="16" /> remove falling objects when they run into them, with the exception of rubies <img src="./tiles/ruby.png" width="16" /> and keys <img src="./tiles/ykey.png" width="16" />.

### ![](./tiles/rock.png) **Rock**
Transmutes to a diamond <img src="./tiles/diamond.png" width="16" />.  
When it falls onto a fragile wall <img src="./tiles/fwall.png" width="16" /> <img src="./tiles/iwall.png" width="16" />, emerald <img src="./tiles/emerald.png" width="16" /> or ice block <img src="./tiles/iblock.png" width="16" />, it removes taht and falls through.  
When it falls onto an egg <img src="./tiles/egg.png" width="16" />, the egg breaks and opens, revealing a diamond <img src="./tiles/diamond.png" width="16" />.  
When it falls on a falling object stuck in glue <img src="./tiles/glue.png" width="16" />, it pushes that object down.  
Can move through quicksand <img src="./tiles/qsand.png" width="16" /> at half speed, but cannot be pushed in or out of it.

### ![](./tiles/qstone.png) **Quantum stone**
Whenever it falls onto something and could roll off both to the left and to the right, it will split into two identical quantum stones and one will roll in each way.  
When hit by a rock <img src="./tiles/rock.png" width="16" /> or orb <img src="./tiles/orb.png" width="16" />, it will also split in two if there's space for that; the falling object will land in between the quantum stone and its duplicate.  
When one quantum stone hits another, the lower one will split if there's room. If not, the upper one may split.  
Transmutes to an emerald <img src="./tiles/emerald.png" width="16" />.  
When its falls onto a fragile wall <img src="./tiles/fwall.png" width="16" /> <img src="./tiles/iwall.png" width="16" />, emerald <img src="./tiles/emerald.png" width="16" /> or ice block <img src="./tiles/iblock.png" width="16" />, it removes that and falls through.  
When it falls onto an egg <img src="./tiles/egg.png" width="16" />, the egg breaks and opens, revealing a diamond <img src="./tiles/diamond.png" width="16" />.  
When it falls on a falling object stuck in glue <img src="./tiles/glue.png" width="16" />, it pushes that object down.

### ![](./tiles/orb.png) **Orb**
When pushed to the side, will keep moving on its own.  
If it hits a wall while rolling, it bounces back and rolls in the other direction.  
If it hits a creature while rolling, it will kill said creature. That includes you <img src="./tiles/player.png" width="16" />.  
If it rolls off a ledge it will fall down and continue rolling once it hits the ground, unless it lands in a corner. It will not bounce after falling.  
Will not fall through transmuters <img src="./tiles/brtrans.png" width="16" />.  
When it falls onto a fragile wall <img src="./tiles/fwall.png" width="16" /> <img src="./tiles/iwall.png" width="16" />, emerald <img src="./tiles/emerald.png" width="16" /> or ice block <img src="./tiles/iblock.png" width="16" />, it removes that and fall through.  
When it falls onto an egg <img src="./tiles/egg.png" width="16" />, or rolls into it from the side, the egg opens to reveal a diamond <img src="./tiles/diamond.png" width="16" />.  
When it rolls into an emerald <img src="./tiles/emerald.png" width="16" /> or ice block <img src="./tiles/iblock.png" width="16" /> from the side, that object is destroyed and the orb bounces back.  
When it falls on a falling object stuck in glue <img src="./tiles/glue.png" width="16" />, it pushes that object down.  
If an orb rolls into one or more stationary orbs, the first one will stay in place and the one at the other end will start rolling.

### ![](./tiles/diamond.png) **Diamond**
Can be picked up; it's worth one point towards the gem quota.  
When picked up, your score increases by a certain amount, which depends on the level you're in.  
When entering a brown transmuter <img src="./tiles/brtrans.png" width="16" />, changes to an emerald <img src="./tiles/emerald.png" width="16" />.  
When entering a blue transmuter <img src="./tiles/bltrans.png" width="16" />, changes to a rock <img src="./tiles/rock.png" width="16" />.  
When hit by a laser (L), it is not removed and instead deflects the laser to the left at a ninety degree angle.  

### ![](./tiles/emerald.png) **Emerald**
Can be picked up; it's worth three points towards the gem quota.  
When picked up, your score increases by twice the value of a diamond.  
When entering a brown transmuter <img src="./tiles/brtrans.png" width="16" />, changes to a rock <img src="./tiles/rock.png" width="16" />.  
When entering a blue transmuter <img src="./tiles/bltrans.png" width="16" />, changes to a bomb <img src="./tiles/bomb.png" width="16" />.  
When hit by a laser (L), it is not removed and instead deflects the laser to the right at a ninety degree angle.  
When a rock <img src="./tiles/rock.png" width="16" />, quantum stone <img src="./tiles/qstone.png" width="16" /> or orb <img src="./tiles/orb.png" width="16" /> falls on top of it, it breaks.  
It also breaks when an orb <img src="./tiles/orb.png" width="16" /> hits it from the side.

### ![](./tiles/ruby.png) **Ruby**
Can be picked up; it's worth five points towards the gem quota.  
When picked up, your score increases by three times the value of a diamond.  
Transmutes to a red key <img src="./tiles/rkey.png" width="16" />.  
Laser (L) beams ignore (pass through) rubies.  
Not affected by explosions <img src="./tiles/explosion.png" width="16" />.  
Unlike other falling objects, it is not eaten by the firedrake. <img src="./tiles/firedrake.png" width="16" />.  
An explosion directly adjacent to a ruby sets off a laser beam in the opposite direction.  
For instance a bomb exploding to the left of a ruby sets off a laser to the right.  
Whenever a ruby is adjacent to a skull <img src="./tiles/skull.png" width="16" />, the skull fires a laser (L) through the ruby.  
If the ruby is stationary this will cause a permanent laser beam.  
If the ruby is in a trap <img src="./tiles/trap.png" width="16" /> at the same moment, the skull will not fire until the trap release button <img src="./tiles/rtrap.png" width="16" /> is pressed.  
You cannot take the ruby from such a permanent laser beam because it's too hot to touch.  
Also, other gems and objects will not roll off a hot ruby; just a quaint property of physics!  

### ![](./tiles/egg.png) **Egg**
Will not fall through brown transmuters <img src="./tiles/brtrans.png" width="16" />.  
When entering a blue transmuter <img src="./tiles/bltrans.png" width="16" />, hatches to a cryo bird <img src="./tiles/cryo.png" width="16" />.  
When hit on top by a rock <img src="./tiles/rock.png" width="16" />, quantum stone <img src="./tiles/qstone.png" width="16" /> or elevator <img src="./tiles/elevator.png" width="16" />, it changes into a diamond <img src="./tiles/diamond.png" width="16" />.  
When hit on top or in the side by an orb <img src="./tiles/orb.png" width="16" />, it changes into a diamond <img src="./tiles/diamond.png" width="16" />.

### ![](./tiles/bomb.png) **Bomb**
Basically it explodes no matter what; take a look:  
When hit on top by anything at all, it explodes.   
Whren it passes through fire <img src="./tiles/fire.png" width="16" />, it explodes.  
When hit from side by an orb <img src="./tiles/orb.png" width="16" />, it explodes.  
When hit by an explosion <img src="./tiles/explosion.png" width="16" /> or laser (L), it explodes.  
When it falls onto a fragile wall <img src="./tiles/fwall.png" width="16" /> <img src="./tiles/iwall.png" width="16" />, it removes that and falls through.  
When it lands on anything else, other than a pillow <img src="./tiles/pillow.png" width="16" />, it explodes.  
WHen landing on an ice block <img src="./tiles/iblock.png" width="16" />, it will remove the ice block by shattering it; also it will explode.  
When it lands on a transmuter <img src="./tiles/brtrans.png" width="16" />, it doesn't transmute but instead explodes.  
The activator <img src="./tiles/activator.png" width="16" /> causes all bombs within range to explode.  
When a firedrake <img src="./tiles/firedrake.png" width="16" /> walks into it, it kills the firedrake and explodes.

### ![](./tiles/mine.png) **Mine**
Transmutes to an emerald <img src="./tiles/emerald.png" width="16" />.  
When hit on top by anything at all, it explodes.  
When hit from the side by an orb <img src="./tiles/orb.png" width="16" />, it explodes.  
When hit by an explosion <img src="./tiles/explosion.png" width="16" /> or a laser (L), it explodes.  
When it lands on anything else, other than a pillow <img src="./tiles/pillow.png" width="16" />, dirt <img src="./tiles/dirt.png" width="16" /> or dirt with gems <img src="./tiles/gdirt.png" width="16" />, it explodes.  
The activator <img src="./tiles/activator.png" width="16" /> causes all mines within range to explode.  
When a firedrake <img src="./tiles/firedrake.png" width="16" /> walks into it, it will explode but the firedrake will escape the explosion.  
Causes small explosions <img src="./tiles/explosion.png" width="16" /> rather than large ones.  
Mines, when linked in a chain, explode twice as fast as a chain of bombs.