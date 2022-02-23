﻿class TicTacToe {

    #id;
    #detects;
    #container;
    #rows;
    #cells;
    #gameId;
    #gameMode;
    #currentPlayer;
    #state;
    #winningCells;
    #aiPrediction;
    #aiPredictionScore;

    constructor(elementId) {

        this.genInstanceId();

        let element = $("#" + elementId);
        if (element && element.length > 0) {
            this.#container = element;
        }
        else {
            throw new Error("Unable to find element with id " + elementId);
        }

        this.initialize();
    }

    log(msg) {
        if (console && console.log)
            console.log(msg);
    }

    genInstanceId() {

        let detects = {
            'ambientlight': function () {
                return Modernizr.ambientlight;
            },
            'batteryapi': function () {
                return Modernizr.batteryapi;
            },
            'blobconstructor': function () {
                return Modernizr.blobconstructor;
            },
            'canvas': function () {
                return Modernizr.canvas;
            },
            'cookies': function () {
                return Modernizr.cookies;
            },
            'cors': function () {
                return Modernizr.cors;
            },
            'cryptography': function () {
                return Modernizr.cryptography;
            },
            'dart': function () {
                return Modernizr.dart;
            },
            'dataview': function () {
                return Modernizr.dataview;
            },
            'emoji': function () {
                return Modernizr.emoji;
            },
            'exiforientation': function () {
                return Modernizr.exiforientation;
            },
            'flash': function () {
                return Modernizr.flash;
            },
            'forcetouch': function () {
                return Modernizr.forcetouch;
            },
            'fullscreen': function () {
                return Modernizr.fullscreen;
            },
            'gamepads': function () {
                return Modernizr.gamepads;
            },
            'geolocation': function () {
                return Modernizr.geolocation;
            },
            'history': function () {
                return Modernizr.history;
            },
            'ie8compat': function () {
                return Modernizr.ie8compat;
            },
            'notification': function () {
                return Modernizr.notification;
            },
            'proximity': function () {
                return Modernizr.proximity;
            },
            'quotamanagement': function () {
                return Modernizr.quotamanagement;
            },
            'touchevents': function () {
                return Modernizr.touchevents;
            },
            'vibrate': function () {
                return Modernizr.vibrate;
            },
            'audioloop': function () {
                return Modernizr.audioloop;
            },
            'audiopreload': function () {
                return Modernizr.audiopreload;
            },
            'webaudio': function () {
                return Modernizr.webaudio;
            }
        }

        Modernizr.addTest(detects);
        this.#detects = detects;

        let instanceId = "";
        if (detects.ambientlight()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.batteryapi()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.blobconstructor()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.canvas()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.cookies()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.cors()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.cryptography()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.dart()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.dataview()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.emoji()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.exiforientation()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.flash()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.forcetouch()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.fullscreen()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.gamepads()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.geolocation()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.history()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.ie8compat()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.notification()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.proximity()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.quotamanagement()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.touchevents()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.vibrate()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.audioloop()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.audiopreload()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        if (detects.webaudio()) {
            instanceId += "1";
        }
        else {
            instanceId += "0";
        }

        instanceId += (new Date()).valueOf();

        this.#id = instanceId;
    }

    getInstanceId() {
        return this.#id;
    }

    get InstanceId() {
        return this.getInstanceId();
    }

    initialize() {
        this.#container.html();
        this.#container.html(
            '<div class="tic-tac-toe-grid">' +
            '    <div class="tic-tac-toe-row-first">' +
            '        <div class="tic-tac-toe-cell-first"></div>' +
            '        <div class="tic-tac-toe-cell"></div>' +
             '       <div class="tic-tac-toe-cell-last"></div>' +
            '    </div>' +
            '    <div class="tic-tac-toe-row">' +
            '        <div class="tic-tac-toe-cell-first"></div>' +
            '        <div class="tic-tac-toe-cell"></div>' +
            '        <div class="tic-tac-toe-cell-last"></div>' +
            '    </div>' +
            '    <div class="tic-tac-toe-row-last">' +
            '        <div class="tic-tac-toe-cell-first"></div>' +
            '        <div class="tic-tac-toe-cell"></div>' +
            '        <div class="tic-tac-toe-cell-last"></div>' +
            '    </div>' +
            '</div>' +
            '<div>' +
            '    <div class="tic-tac-toe-display">' +
            '    </div>' +
            //'    <div style="clear: both;">' +
            //'    </div>' +
            '    <div class="tic-tac-toe-controls">' +
            '        <button type="button" class="btn btn-danger reset">Reset</button>' +
            '        <button type="button" class="btn btn-danger changeMode">Change Mode</button>' +
            '        <button type="button" class="btn btn-info aiPrediction">AI Prediction</button>' +
            '    </div> ' +
            '</div>'
        );

        this.#rows = this.#container.find("div.tic-tac-toe-grid").children("[class*='tic-tac-toe-row']");
        this.#cells = this.#rows.children("[class*='tic-tac-toe-cell']");
        this.#gameId = (new Date()).valueOf();
        let readGameMode = parseInt(sessionStorage.getItem(this.InstanceId + "gameMode"));
        this.#gameMode = readGameMode === 1 || readGameMode === 2 ? readGameMode : 1;
        this.#currentPlayer = 1;
        this.#state = 0;
        this.#winningCells = [];
        this.#aiPrediction = 0;
        this.#aiPredictionScore = 0;

        let showPrediction = parseInt(sessionStorage.getItem(this.InstanceId + 'showPrediction'));
        if (isNaN(showPrediction)) {
            sessionStorage.setItem(this.InstanceId + 'showPrediction', 1);
        }

        this.#cells.css("background-image", "url('./Images/Tic-Tac-Toe/CrossRed120x120.png')");
        this.#cells.css("background-image", "url('./Images/Tic-Tac-Toe/CrossRed60x60.png')");
        this.#cells.css("background-image", "url('./Images/Tic-Tac-Toe/Cross120x120.png')");
        this.#cells.css("background-image", "url('./Images/Tic-Tac-Toe/Cross60x60.png')");
        this.#cells.css("background-image", "url('./Images/Tic-Tac-Toe/CircleRed120x120.png')");
        this.#cells.css("background-image", "url('./Images/Tic-Tac-Toe/CircleRed60x60.png')");
        this.#cells.css("background-image", "url('./Images/Tic-Tac-Toe/Circle120x120.png')");
        this.#cells.css("background-image", "url('./Images/Tic-Tac-Toe/Circle60x60.png')");
        this.#cells.css("background-image", "");
        
        this.bindEvents();
        this.refreshUI();
    }

    bindEvents() {

        let cells = this.#cells;

        for (var i = 0; i < cells.length; i++) {
            $(cells[i]).attr("id", "cell" + i);
            $(cells[i]).attr("data-state", 0);
        }

        cells.off();
        cells.on('click', this, this.cellClicked);

        $(window).resize(this, function (event) {

            let app = event.data;
            app.refreshUI();
        });

        this.#container.find("button.aiPrediction").off();
        this.#container.find("button.aiPrediction").on('click', this, function (event) {
            let app = event.data;

            let showPrediction = parseInt(sessionStorage.getItem(app.InstanceId + 'showPrediction'));

            if (showPrediction === 1)
                showPrediction = 0;
            else
                showPrediction = 1;

            sessionStorage.setItem(app.InstanceId + 'showPrediction', showPrediction);
            app.refreshUI();
        });

        this.#container.find("button.changeMode").off();
        this.#container.find("button.changeMode").on('click', this, function (event) {
            let app = event.data;
            if (app.#gameMode == 1)
                app.#gameMode = 2;
            else
                app.#gameMode = 1;
            sessionStorage.setItem(app.InstanceId + 'gameMode', app.#gameMode);
            app.initialize();
        });

        this.#container.find("button.reset").off();
        this.#container.find("button.reset").on('click', this, function (event) {
            let app = event.data;
            app.initialize();
        });
    }

    cellClicked(event) {

        let app = event.data;
        //app.log(event);
        //app.log($(event.target));
        //app.log("cell " + $(event.target).attr("id") + " clicked");

        if (app.#state == 0) {
            let cellState = new Number($(event.target).attr("data-state"));
            if (cellState == 0) {
                $(event.target).attr("data-state", app.#currentPlayer);

                if (app.#gameMode == 2) {
                    if (app.#currentPlayer == 1) {
                        app.#currentPlayer = 2;
                    } else {
                        app.#currentPlayer = 1;
                    }
                }
                else {
                    app.#currentPlayer = 1;
                }

                app.checkResult();
                app.refreshUI.call(app);
            }
        }
    }

    refreshUI() {

        let rows = this.#rows;
        let cells = this.#cells;

        let showPrediction = parseInt(sessionStorage.getItem(this.InstanceId + 'showPrediction'));
        if (isNaN(showPrediction))
            showPrediction = 1;

        let screenWidth = $(window).width();
        
        let height = screenWidth >= 480 ? 120 : 60;
        let width = screenWidth >= 480 ? 120 : 60;
        let paddingWidth = 6;
        let paddingHeight = 6;
        rows.css("height", height + paddingWidth + "px");
        cells.css("width", width + paddingHeight + "px");

        for (var i = 0; i < cells.length; i++) {

            let cell = $(cells[i]);
            let isWinningCell = this.#winningCells.includes(i);
            let color = isWinningCell ? 'Red' : '';

            let cellState = cell.attr("data-state");
            if (cellState == 1) {
                cell.css("background-image", "url('./Images/Tic-Tac-Toe/Cross" + color + width + "x" + height + ".png')");
            } else if (cellState == 2) {
                cell.css("background-image", "url('./Images/Tic-Tac-Toe/Circle" + color + width + "x" + height + ".png')");
            } else {
                cell.css("background-image", "");
            }
        }

        let msg = '';
        let img1Player = '1P30x20.png';
        let img2Player = '2P30x20.png';
        let imgPlayer1 = 'Cross20x20.png';
        let imgPlayer2 = 'Circle20x20.png';

        if (screenWidth >= 480) {
            img1Player = '1P45x30.png';
            img2Player = '2P45x30.png';
            imgPlayer1 = 'Cross30x30.png';
            imgPlayer2 = 'Circle30x30.png';
        }

        if (this.#state == 0) {
            if (this.#gameMode == 1) {
                msg =
                    '<div class="clearfix">' +
                    '   &nbsp;' +
                    '   <div class="gameMode">' +
                    '       <img src="./Images/Tic-Tac-Toe/' + img1Player + '" alt="1 Player" />' +
                    '   </div>' +
                    '   <div class="player2">' +
                    '       <div>' +
                    '           <img src="./Images/Tic-Tac-Toe/' + imgPlayer2 + '" alt="O" />' +
                    '       </div>' +
                    '       <div>' +
                    '           Computer' +
                    '       </div>' +
                    '   </div>' +
                    '   <div class="player1">' +
                    '       <div>' +
                    '           <img src="./Images/Tic-Tac-Toe/' + imgPlayer1 + '" alt="X" />' +
                    '       </div>' +
                    '       <div>' +
                    '           You' +
                    '       </div>' +
                    '   </div>' +
                    '</div>';
            }
            else {
                msg =
                    '<div class="clearfix">' +
                    '   &nbsp;' +
                    '   <div class="gameMode">' +
                    '       <img src="./Images/Tic-Tac-Toe/' + img2Player + '" alt="2 Players" />' +
                    '   </div>' +
                    '   <div class="player1">' +
                    '       <div>' +
                                (this.#currentPlayer == 1
                                ? '<img src="./Images/Tic-Tac-Toe/' + imgPlayer1 + '" alt="X" />'
                                : '<img src="./Images/Tic-Tac-Toe/' + imgPlayer2 + '" alt="O" />') +
                    '       </div>' +
                    '       <div>' +
                    '           Current Player' +
                    '       </div>' +
                    '   </div>' +
                    '</div>';
            }

            if (showPrediction == 1 && this.#aiPrediction > 0 && this.#aiPredictionScore > 0) {

                let aiPredictionMsg = '<div>' + (this.#aiPredictionScore * 100).toFixed(2) + '% probability ' + (this.#aiPrediction == 3 ? 'draw' : 'Player ' + this.#aiPrediction + ' wins') + '</div>';
                msg = msg + aiPredictionMsg;
            }
        }
        else if (this.#state == 1) {
            msg = '<div style="color: red">Player 1 Wins</div>';
        }
        else if (this.#state == 2) {
            msg = '<div style="color: red">Player 2 Wins</div>';
        }
        else {
            msg = '<div>Draw</div>';
        }

        let gridWidth = (width + paddingWidth + paddingWidth) * 3;

        let divTicTacToeDisplay = this.#container.find("div.tic-tac-toe-display");
        divTicTacToeDisplay.html(msg);
        divTicTacToeDisplay.width(gridWidth);

        let descendantDivs = divTicTacToeDisplay.find("div");
        descendantDivs.css('font-size', screenWidth >= 480 ? '24px' : '12px');
        descendantDivs.css('font-weight', screenWidth >= 480 ? 'bold' : 'bold');

        let divTicTacToeControls = this.#container.find("div.tic-tac-toe-controls");
        divTicTacToeControls.width(gridWidth);

        this.#container.find("button.aiPrediction").text("AI Prediction " + (showPrediction == 1 ? "On" : "Off"));
        
        //div.tic - tac - toe - display {
        //    width: 360px;
        //    height: 50px;
        //    padding - top: 5px;
        //}

        //div.tic - tac - toe - display div{
        //    font - size: 24px;
        //    font - weight: 500;
        //    vertical - align: middle;
        //}

        //div.tic - tac - toe - display div div{
        //    font - size: 24px;
        //    font - weight: 500;
        //    vertical - align: middle;
        //}

        //div.gameMode {
        //    float: left;
        //}

        //div.gameMode div{
        //    display: inline - block;
        //    padding: 0px 5px 0px 5px;
        //}

        //div.player1 {
        //    float: right;
        //    padding - left: 10px;
        //}

        //div.player1 div {
        //    display: inline - block;
        //    padding: 0px 5px 0px 5px;
        //}

        //div.player2 {
        //    float: right;
        //    padding - left: 10px;
        //}

        //div.player2 div {
        //    display: inline - block;
        //    padding: 0px 5px 0px 5px;
        //}

        //div.tic - tac - toe - controls {
        //    width: 360px;
        //}
    }

    checkResult() {

        let unusedCells = this.#cells.filter("[data-state=0]");
        if (unusedCells.length == 0) {
            this.#state = 3;
        }

        let requestData =
        {
            InstanceId: "" + this.#gameId,
            GridSize: 3,
            NumberOfPlayers: this.#gameMode,
            CellStates: []
        };

        for (var i = 0; i < this.#cells.length; i++) {
            let cell = $(this.#cells[i]);
            let cellState = new Number(cell.attr("data-state"));
            requestData.CellStates.push(cellState);
        }

        let requestJson = JSON.stringify(requestData);
        let app = this;

        $.ajax(
            "./api/TicTacToe",
            {
                async: false,
                method: "post",
                contentType: "application/json",
                data: requestJson,
                success: function (resp) {
                    app.log(resp);
                    app.#state = resp.Status;
                    app.#winningCells = [];
                    app.#aiPrediction = 0;
                    app.#aiPredictionScore = 0;

                    //debugger;
                    if (resp.WinningCells && Array.isArray(resp.WinningCells)) {
                        app.#winningCells = resp.WinningCells;
                    }

                    if (app.#gameMode == 1) {
                        //debugger;
                        let computerMove = parseInt(resp.ComputerMove);
                        if (typeof computerMove === 'number' && computerMove >= 0 && computerMove < app.#cells.length) {
                            let cellToChange = $(app.#cells[computerMove]);
                            if (cellToChange && cellToChange.length > 0)
                                cellToChange.attr("data-state", 2);
                        }

                        let prediction = parseInt(resp.Prediction);
                        let predictionScore = resp.PredictionScore;

                        if (!isNaN(prediction) && Array.isArray(predictionScore)) {
                            let tempArray = [];
                            for (var i = 0; i < predictionScore.length; i++) {
                                let tempFloat = parseFloat(predictionScore[i]);
                                if (!isNaN(tempFloat)) {
                                    tempArray.push(tempFloat);
                                }
                            }
                            let maxScore = Math.max(...tempArray);
                            app.#aiPrediction = prediction;
                            app.#aiPredictionScore = maxScore;
                        }
                    }
                }
            }
        );
    }
}