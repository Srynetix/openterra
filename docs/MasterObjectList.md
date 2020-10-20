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