class TicTacToe {

    #id;
    #detects;
    #container;
    #cells;
    #currentPlayer;
    #state;

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
            '    <div class="display">' +
            '    </div > ' +
            '    <div>' +
            '        <button type="button" class="btn btn-danger reset">Reset</button>' +
            '    </div > ' +
            '</div>'
        );

        this.#cells = this.#container.find("div.tic-tac-toe-grid").children().children();
        this.#currentPlayer = 1;
        this.#state = 0;
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
            }

            if (app.#currentPlayer == 1) {
                app.#currentPlayer = 2;
            } else {
                app.#currentPlayer = 1;
            }

            app.checkResult();
            app.refreshUI.call(app);
        }
    }

    refreshUI() {

        let cells = this.#cells;

        for (var i = 0; i < cells.length; i++) {

            let cell = $(cells[i]);

            let cellState = cell.attr("data-state");
            if (cellState == 1) {
                cell.css("background-image", "url('./Images/Tic-Tac-Toe/Cross120x120.png')");
            } else if (cellState == 2) {
                cell.css("background-image", "url('./Images/Tic-Tac-Toe/Circle120x120.png')");
            } else {
                cell.css("background-image", "");
            }
        }

        let msg = '';

        if (this.#state == 0) {
            msg = '<span>Current Player</span>&nbsp;<label class="currentPlayer">' + this.#currentPlayer + '</label>';
        }
        else if (this.#state == 1) {
            msg = '<label>Player 1 wins</label>';
        }
        else if (this.#state == 2) {
            msg = '<label>Player 2 wins</label>';
        }
        else {
            msg = '<label>Draw</label>';
        }

        this.#container.find("div.display").html(msg);
    }

    checkResult() {

        let unusedCells = this.#cells.filter("[data-state=0]");
        if (unusedCells.length == 0) {
            this.#state = 3;
        }

        let requestData =
        {
            GridSize: 3,
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
                }
            }
        );
    }
}