/*jslint browser: true, devel: true, node: true, sloppy: true, regexp: true */
/*global $, WGo */

$(function () {
    var board = new WGo.Board(document.getElementById("board"), { width: 600 }),
        tool = document.getElementById("tool"),
        game = new WGo.Game(19, "ALL", false, false); // get the <select> element
    
    board.addEventListener("click", function (x, y) {
        if (tool.value === "black") {
            board.addObject({
                x: x,
                y: y,
                c: WGo.B
            });
        } else if (tool.value === "white") {
            board.addObject({
                x: x,
                y: y,
                c: WGo.W
            });
        } else if (tool.value === "remove") {
            board.removeObjectsAt(x, y);
        } else {
            board.addObject({
                x: x,
                y: y,
                type: tool.value
            });
        }
    });
});