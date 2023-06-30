# Parry-Knight
## Introduction
Hello! This is my small game, Parry Knight. I once played a top-down 2D shooter, one of the Vampire Survivor clones... 
I thought to myself. These types of games are always focused on the damage output you accumulate over time.
They also stress the stacking of health and defenses. But they don't give you the possibility to reflect the damage that targets you.
Also, the ability to take out an enemy using the power of their weapon is always fun in my opinion.
So, I took it upon myself to spend some days working on this game, and here is what I came out with.
## Mechanics
In this section. I'll talk about the mechanics of the game. Let's start with :
### The parry mechanic
This mechanic is the base behavior that's demanded from the player to survive. 
How is it performed you ask?
By pressing the **Shift key** and pointing the cursor toward the direction you wish the projectile to be launched in.

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/9927bf91-4337-4b20-94ad-efa3075c944d)

But it's not as simple as that!

#### Perfect Timing!

In order to reflect the projectile you need perfect timing. You need to press the **Shift** button just as the projectile is about to hit your character.
The perfect timing window is not fixed as you level up you'll be able to increase it.



#### Leveling up

In this game, you gain experience by reflecting the projectiles away from you. When you collect enough to level up, you'll get the possibility to increase your stats.

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/2b3a3960-5a55-48a6-8513-f5be9850dd06)

#### Juggling projectiles

As I mentioned before, the parry mechanic is a little complex. You can parry a projectile multiple times. 
Each time you increase its damage and that is noticed through a change in the color of its trail.

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/2bb210af-883d-4c02-ba2e-5d2aa23e71c0)


Some of you might ask why are the colors listed on the projectile and not on the shield itself.
Well, I have different projectile types, I have **rock** and **arrow** thrown by respectively by an **archer** and a **rock-thrower** enemy.



#### Reduced Damage
When you miss the perfect block timing, you can get some reduced damage by holding the shield up to your body. This could be used as a way to at least be able to fend off some damage even without reflecting the projectiles.
Also, your movement speed is reduced when the shield is up whether you are looking for a perfect block or looking for reduced damage.



#### Blood

It seems weird to mention visual effects in the mechanics section, but they do serve a purpose.
When you are hit with a projectile, blood splatters on the screen. It's portrayed by red pixels on the screen and a vignette getting tighter and redder the more damage you take. This in turn makes navigation harder and detecting the trajectory of each enemy's attack more confusing.

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/0facff81-f4ad-42b3-b94a-9b4ed6932ac3)



#### Healing Item

These are pickable items that restore your health and clean up any visual impairments you have. And of course, show you some cool particle effects that I made.

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/f26bfc80-2e65-422b-b863-b12dbd82b2a9)


## ART

### Environment and characters

I'm not an artist, I excel at programming but even simple pixel art is quite hard for me.


I've used this free pack of really beautiful and artistic characters. 
Here is the link:
https://anokolisa.itch.io/dungeon-crawler-pixel-art-asset-pack

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/a5f4e31f-67d4-4e28-8030-a70392ac8924)


### UI
Once I had more time to work on the game, I decided to contribute to the art with some UI and buttons and icons that you'll find implemented like that health bar and many other things.


![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/2dc1a45e-5476-4860-9abd-41784e208349)

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/1f86927e-b956-4c33-8a90-27800b435d2f)

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/729c2db4-a808-4e54-9c81-2a2500f1f730)

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/76865d26-7646-4980-beb1-1b5c13f6b4e1)

## Systems

### Events Management

Most of the things that I worked on are based on the event manager that I made. You'll find it as a public repository on my account.

**But for a quick rundown of how it works here is a little tutorial:**


![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/694d4dc4-3903-4934-9834-6d39090f42c7)


I have a list of game objects that are listening to the events that I'll call through scripts or the inspector.


![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/a77b3789-d57a-4637-9589-46e66bb3c59a)

This is an example of an event that is called whenever the player takes damage.
It's a float game event which means that it takes and distributes a float to all its subscribed functions.

It helped me create this game in less than a week. If I opted to use the regular methods of calling events by writing entire scripts for one or two methods, I would have had quite the entangled architecture.
The focus of this event system is the ability to increase modularity and dependency. But I did rely on the singelton pattern a little more than usual in this project with my managers.


### Audio

I decided to not have an audio manager and instead use a script that controls the walking sfx for the player. Then call other sound effects using the event manager I mentioned before.

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/203ff491-dcb6-4985-b03f-6d639559169e)

### Pooling

I tried to use some pooling in order to reduce the number of projectiles and I used a very simplistic implementation but I guess that's enough to prioritize performance.

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/24f51ed3-cb5b-45ed-88b2-169d169f7e0e)

I also used the same approach to the particles and damage numbers that show up on impact : 

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/4b16a670-ba6e-4802-8a47-ce380e21130e)


### Leveling 


Since leveling in this type of game is simple, I decided to implement a milestone-based approach. Something predefined that the player has to reach by blocking and reflecting projectiles.
The leveling manager is also linked to the panel in which the upgrade cards show up. And also linked to the powerup effect in case I implement a way to increase the intensity of the particle system based on the level reached.


![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/777df624-9ead-46c3-a61a-95be14d14bc4)



### Animating
When working with the usual animator and prefab workflow, I found it very tedious to just have many enemies with many animators and prefabs so I decided to dynamically assign animations using a system that I created.
It's not perfect. But here is how it works:

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/ff743e1f-066a-4713-b3c2-614c38d0118a)

This component holds references to scriptable objects that hold many of the frames that constitute an animation.

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/8e6ff5c5-10d1-47b4-bf05-265628bd1f68)

I only need one animator assigned to the enemy character.

![image](https://github.com/YassinDhahbi/Parry-Knight/assets/90442257/6fc1549d-77a8-4fdc-9239-3855e78a0c7f)

During each animation, I spread the events based on how the frames are laid out. Then each event is called and it assigns the frame selected from the current animation data provided by the currently selected animation scriptable object

## Conclusion 

* Working on this game was quite a nice change of pace from the harder 3D workflow that I use on most of the weekdays.

* I loved working on 2D since I rarely get the chance to do so. I liked the fact that I was able to make some pixel art that was useful for the UI. 

* I can't stress enough the fact that a game developer should spend some time playing games and making them. Because if it weren't for that combination I wouldn't have found the idea for this game.






