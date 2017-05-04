/*jslint browser: true, devel: true, node: true, sloppy: true, regexp: true */
/*global $, WGo */

$(function () {
    var board = new WGo.Board(document.getElementById("board"), { width: 600, stoneHandler : WGo.Board.drawHandlers.PAINTED, section: {
            top: -0.5,
            left: -0.5,
            right: -0.5,
            bottom: -0.5
        } }),
        tool = document.getElementById("tool"),
        game = new WGo.Game(19, "ALL", false, false),
        coordinates = {
        // draw on grid layer
            grid: {
                draw: function (args, board) {
                    var ch, t, xright, xleft, ytop, ybottom, i;

                    this.fillStyle = "rgba(0,0,0,0.7)";
                    this.textBaseline = "middle";
                    this.textAlign = "center";
                    this.font = board.stoneRadius + "px " + (board.font || "");

                    xright = board.getX(-0.75);
                    xleft = board.getX(board.size - 0.25);
                    ytop = board.getY(-0.75);
                    ybottom = board.getY(board.size - 0.25);

                    for (i = 0; i < board.size; i += 1) {
                        ch = i + "A".charCodeAt(0);
                        if (ch >= "I".charCodeAt(0)) {
                            ch += 1;
                        }

                        t = board.getY(i);
                        this.fillText(board.size - i, xright, t);
                        this.fillText(board.size - i, xleft, t);

                        t = board.getX(i);
                        this.fillText(String.fromCharCode(ch), t, ytop);
                        this.fillText(String.fromCharCode(ch), t, ybottom);
                    }

                    this.fillStyle = "black";
                }
            }
        };
    
    console.log(board);
    
    board.addCustomObject(coordinates);
    
    board.addEventListener("mousemove", function (x, y) {
        console.log(x, y);
    });
    
    board.addEventListener("click", function (x, y) {
        var i, j, position, stone;
        
        board.addObject({
            x: x,
            y: y,
            c: game.turn
        });
        
        game.play(x, y);
        
       // position = game.getPosition();
        
        for (i = 0; i < 19; i += 1) {
            for (j = 0; j < 19; j += 1) {
                //console.log(position.get(i, j));
                stone = game.getStone(i, j);
                if (stone === 0) {
                    board.removeObjectsAt(i, j);
                } else {
                    board.addObject({
                        x : i,
                        y : j,
                        c : game.getStone(i, j)
                    });
                }
            }
        }
        
        /*if (tool.value === "black") {
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
        }*/
    });
});