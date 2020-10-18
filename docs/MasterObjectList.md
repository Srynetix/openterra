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