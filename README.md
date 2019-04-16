# MaJu-MOHO

(work in progress)
This is a 2D futuristic volleyball game project. Hit the ball across the opponents defense,
through their goal. 
The ball is not subject to gravity, you are, it passes through thes walls, you does not.
Nervous and speedy gameplay for an entertaining local party game. 
	___________

This project is a personnal work i have done with my roommate (Julien Mounthanyvoung) on our
free time since february 2019.
The purpose here is to get used to the Unity interface, and to practice Object oriented 
programming.
	###########################################
player 1 

move : zsqd
jump : space
dash : o
aim  : k (hold)
shoot : k (release)
rise shield : p

	###########################################

The major coding part is focused on the main character behavior (move, animation, objects
interractions). I wanted to create the game "from scratch". So I designed my own character, 
introduced collider, rigid body, wall and ground detection zones. I put some platforms, and
learned how to use inputs to make the character move, jump, dash, jump-off. All those actions 
are developped in the "character1Controller" script.

Then, I added the ball, implemented various functions "aim", and "Smatch" that allow the 
player floating in the air while charging a shoot. 

Creating the goal zones made me write event and action abstract class scripts for collision
management, system I then resused to implement a stun action when the player get hit by a 
too fast ball. 

Parallel operation, I designed a basic main menu in order to do character and map selection.

	____________

