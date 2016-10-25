# PlayingWithGraphs

This is a simple application primarily for personal educational purposes. It allows for the rapid creation and tinkering with simple graphs. 

server side: C# .NET MVC framework, MYSQL database
client side: javascript/jQuery with jsPlumb toolkit

Usage:
Create a new graph or select existing one
left click creates a new graph node
left clicking a node allows for renameing (note, atm it requires move the node slightly or reloading to get the connections to update for size)
right clicking a node deletes it and all connected connections
left click and drag to move nodes around

All changes persist. 

Future plans:
Allow deleting of individual connections
Add cycle detection option
Add shortest path option
Add weights to connections + label in view
