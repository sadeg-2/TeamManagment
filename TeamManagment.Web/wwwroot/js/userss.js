"use strict";


// sadeg
const userId = "e9896a0c-4833-4043-85ea-33a8fa41068a";


// dev id 8f014522-2dab-43f4-b02a-9d364d6b0138
// sadeg id e9896a0c-4833-4043-85ea-33a8fa41068a
//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
var connection = new signalR.HubConnectionBuilder().withUrl("/NotifyHub").build();
connection.on("ReciveNotify", function (notify) {
    debugger
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    alert("hehe");
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${notify.title} says ${notify.message} with hiis Id ${notify.userId}`;
});

connection.start().then(function () {
    console.log("connection done");
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
//connection.on("ReceiveFromUser", function (user, message, reciverId) {
//    debugger
//    var li = document.createElement("li");
//    document.getElementById("messagesList").appendChild(li);
//    alert("hehe");
//    // We can assign user-supplied strings to an element's textContent because it
//    // is not interpreted as markup. If you're assigning in any other way, you 
//    // should be aware of possible script injection concerns.
//    li.textContent = `${user} says ${message} with hiis Id ${reciverId}`;
//});

//connection.start().then(function () {
//    console.log("connection done");
//    document.getElementById("sendButton").disabled = false;
//}).catch(function (err) {
//    return console.error(err.toString());
//});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message, userId).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});