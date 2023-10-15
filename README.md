# KitchenChaosExample v1.0
Cooking game made with Unity and following Code Monkey's tutorial. 

Cooking simulator where you'll have to fulfill as much orders as you possibly can within 60 seconds.

## Features

+ Main Title scene:
    + Ability to start game or quit
    + Automatically load Player Preferences
+ Loading scene:
    + Custom Loader to ease transition between scenes
+ Game scene:
    + Dynamic tutorial window UI:
        + Key rebindings are updated to show player's preference
        + Only responds to allowed input
    + Game start countdown with animation
    + Clean UI:
        + Clock showing time left
        + List of recipes to deliver:
            + Show name and ingredients
            + Only 4 recipes allowed at the same time
            + Randomly spawn after a delay
    + Player controller:
        + Can move in all directions
        + Animated and with a particle system
        + Raycast to interact and collide with counters
        + Can pick up and drop objects
    + Variety of counters:
        + Clear counter to hold objects
        + Spawn counter to spawn new ingredients
        + Trash counter to destroy objects
        + Cutting counter:
            + Play cutting animation
            + Change object into another according to a recipe Scriptable Object
            + Progress bar UI
        + Stove counter:
            + Cooking counter for meat only
            + Can fry and burn meat
            + State machine to control flow
            + Progress bar UI and burning warning animation
        + Plate counter to spawn plates
        + Delivery counter:
            + Check if recipe delivered is right or wrong
            + Display animated visual
    + Pause menu:
        + Resume game
        + Open options menu
        + Go back to main title
    + Options menu:
        + Change SFX and music volume
        + Changes persist through sessions
        + Rebind keys

Scriptable objects, events, delegates, state machines, singletons, new Player Input System, animations and more have been leveraged.

