# CrossX
## Clipboard Model: 
https://assetstore.unity.com/packages/3d/props/clipboard-137662

## Board and Pen Scripts:
https://www.cnblogs.com/marsir/p/8435240.html
First of all, I want to apologize because I checked again and found that I did follow this tutorial to implement the whiteboard feature. I’ve done a few Unity projects before and put some interesting features into my own repository, some of which are only vaguely remembered and I remember spending some time on them.  T-T

This tutorial is intended to run in the context of VRTK and Unity 5, either of which my project does not meet. The main thing I did was migrate it to Unity 2020. At the time I spent considerable time and energy dealing with conflicts, creating demos, and trying to rewrite code. I was so familiar with it that I did not recall that it didn’t come from me when I was working on the project.

## Environment, Building structure and some furniture: https://assetstore.unity.com/packages/3d/environments/flooded-grounds-48529
Most of the game scene comes from a building in this Asset. I only made some modifications like some of the furniture’s sizes and positions has been rearranged to fit the VR experience, and some of the improper lighting has been altered. I also animated some of the props in the scene (such as doors and the key) to interact with the player or to respond to the process.

## Zombie Prefab and animator: 
https://assetstore.unity.com/packages/3d/characters/humanoids/zombie-30232

## Key Prefab:
https://assetstore.unity.com/packages/3d/props/rust-key-167590

## Gun Model, Animator and Basic Scripts:
https://assetstore.unity.com/packages/3d/props/weapons/low-poly-fps-pack-free-sample-144839

The original model comes from a 3D FPS, which includes character control, gun shooting and sound effects. I modified it, removing unwanted models and features (such as melee weapons and character movement), adding some colliders and grabbers, and adjusting some parameters (such as bullet mass and velocity). I also rewrote the shooting part of the code and replaced the keyboard part with the input from the VR device, and it’s written a new firing process for VR (controlling the rate of fire, making it more like a pistol than an Automatic rifle).

## VR Beginner:
https://assetstore.unity.com/packages/templates/tutorials/vr-beginner-the-escape-room-163264

My basic understanding of VR projects and the first half of the game were inspired by this tutorial.

And for the record, I initially thought the XR Rig component it provided was Unity’s native component, so I used it directly. Now I found out that its MasterController component belongs to this asset, but I didn’t use its extra features.

What I do in this part is set up the collider for the character and write the code for recognizing device inputs and making the character move.

## FPS Creator Kit:
https://learn.unity.com/project/chuang-zuo-zhe-tao-jian-fps
The tutorial you recommended for me, the inspiration for the second half of the game, did not use specific resources or codes.

