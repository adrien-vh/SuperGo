/*jslint browser: true, devel: true, node: true, sloppy: true, regexp: true */
/*global $, WGo */

$(function () {
    var board = new WGo.Board(document.getElementById("board"), { width: 600, stoneHandler : WGo.Board.drawHandlers.SHELL, overAlpha : 0.7, section: {
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
        },
        plane = {
        // draw on stone layer
            over: {
                // draw function is called in context of CanvasRenderingContext2D, so we can paint immediately using this
                draw: function (args, board) {
                    
                   /* var xr = board.getX(args.x), // get absolute x coordinate of intersection
                        yr = board.getY(args.y), // get absolute y coordinate of intersection
                        sr = board.stoneRadius; // get field radius in px*/
                    
                    args.c = game.turn;
                    
                    this.clearRect(0, 0, board.width, board.height);
                    
                    if (game.getStone(args.x, args.y) === 0) {
                        board.stoneHandler.stone.draw.call(this, args, board);
                    }
                    
                    /*// if there is a black stone, draw white plane
                    if (board.obj_arr[args.x][args.y][0].c === WGo.B) {
                        this.strokeStyle = "white";
                    } else {
                        this.strokeStyle = "black";
                    }

                    this.lineWidth = 3;

                    this.beginPath();

                    this.moveTo(xr - sr * 0.8, yr);
                    this.lineTo(xr + sr * 0.5, yr);
                    this.lineTo(xr + sr * 0.8, yr - sr * 0.25);
                    this.moveTo(xr - sr * 0.4, yr);
                    this.lineTo(xr + sr * 0.3, yr - sr * 0.6);
                    this.moveTo(xr - sr * 0.4, yr);
                    this.lineTo(xr + sr * 0.3, yr + sr * 0.6);

                    this.stroke();*/
                }
            }
        };
        

    console.log(board);
    
    
    board.addCustomObject(coordinates);
    
    
    board.addEventListener("mousemove", function (x, y) {
        board.addObject({
            x: x,
            y: y,
            type: plane
        });
        
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