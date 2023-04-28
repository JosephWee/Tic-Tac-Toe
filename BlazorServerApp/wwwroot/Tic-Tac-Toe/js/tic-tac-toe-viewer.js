class TicTacToeViewerJq {

    #id;
    #container;
    #rows;
    #cells;
    #gameId;
    #description;
    #state;
    #winningCells;
    #getTicTacToeResultSetDelegate;

    constructor(elementId) {

        /*
        // Test genUniqueId
        let d = {};
        for (var i = 0; i < 1000; i++) {
            let key = this.genUniqueId();
            if (d[key]) {
                d[key] += 1;
            }
            else {
                d[key] = 1;
            }
        }

        let keys = Object.keys(d);
        if (console && console.log)
            console.log("uniqueIds count: " + keys.length);
        for (var i = 0; i < keys.length; i++) {
            let key = keys[i];
            let count = d[key];
            if (count > 1 && console && console.log)
                console.log(key + ": " + count);
        }
        */

        this.#id = this.genUniqueId();

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

    genUniqueId() {
        let a = Math.floor(Math.random() * 100000);
        let b = Math.floor(Math.random() * 10000);
        let c = Math.floor(Math.random() * 1000);
        let d = Math.floor(Math.random() * 100);
        let e = Math.floor(Math.random() * 10);
        let ua = {};
        for (var i = 0; i < navigator.userAgent.length; i++) {
            let ch = navigator.userAgent.charCodeAt(i);
            let k = "" + ch;
            if (ua[k]) {
                ua[k] += 1;
            }
            else {
                ua[k] = 1;
            }
        }

        let sum = 0;
        let keys = Object.keys(ua);
        for (var i = 0; i < keys.length; i++) {
            let key = keys[i];
            sum += parseInt(key) * ua[key];
        }

        let uniqueId = ((new Date()).valueOf() + ((a + b + c + d + e) * sum));
        //if (console && console.log)
        //    console.log(uniqueId);

        return uniqueId;
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
            '<div class="tic-tac-toe-viewer">' +
            '   <div class="tic-tac-toe-viewer-row">' +
            '       <div class="tic-tac-toe-viewer-cell">' +
            '           <button type="button" class="btn btn-viewer-nav prev">Prev</button>' +
            '       </div>' +
            '       <div class="tic-tac-toe-viewer-cell">' +
            '           <div class="tic-tac-toe-grid">' +
            '               <div class="tic-tac-toe-row-first">' +
            '                   <div class="tic-tac-toe-cell-first"></div>' +
            '                   <div class="tic-tac-toe-cell"></div>' +
            '                   <div class="tic-tac-toe-cell-last"></div>' +
            '               </div>' +
            '               <div class="tic-tac-toe-row">' +
            '                   <div class="tic-tac-toe-cell-first"></div>' +
            '                   <div class="tic-tac-toe-cell"></div>' +
            '                   <div class="tic-tac-toe-cell-last"></div>' +
            '               </div>' +
            '               <div class="tic-tac-toe-row-last">' +
            '                   <div class="tic-tac-toe-cell-first"></div>' +
            '                   <div class="tic-tac-toe-cell"></div>' +
            '                   <div class="tic-tac-toe-cell-last"></div>' +
            '               </div>' +
            '           </div>' +
            '           <div class="tic-tac-toe-description"></div>' +
            '           <div>' +
            '               <div class="tic-tac-toe-display">' +
            '               </div>' +
            '               <div class="tic-tac-toe-pager">' +
            '               </div>' +
            '               <div style="clear: both;"></div>' +
            '           </div>' +
            '       </div>' +
            '       <div class="tic-tac-toe-viewer-cell">' +
            '           <button type="button" class="btn btn-viewer-nav next">Next</button>' +
            '       </div>' +
            '   </div>' +
            '</div>'
        );

        this.#rows = this.#container.find("div.tic-tac-toe-grid").children("[class*='tic-tac-toe-row']");
        this.#cells = this.#rows.children("[class*='tic-tac-toe-cell']");
        this.#gameId = this.genUniqueId();
        this.#state = 0;
        this.#winningCells = [];

        sessionStorage.setItem(this.InstanceId + "PageNum", "1");
        sessionStorage.setItem(this.InstanceId + "PageCount", "1");

        this.#cells.css("background-image", "url('/Tic-Tac-Toe/CrossRed120x120.png')");
        this.#cells.css("background-image", "url('/Tic-Tac-Toe/CrossRed60x60.png')");
        this.#cells.css("background-image", "url('/Tic-Tac-Toe/Cross120x120.png')");
        this.#cells.css("background-image", "url('/Tic-Tac-Toe/Cross60x60.png')");
        this.#cells.css("background-image", "url('/Tic-Tac-Toe/CircleRed120x120.png')");
        this.#cells.css("background-image", "url('/Tic-Tac-Toe/CircleRed60x60.png')");
        this.#cells.css("background-image", "url('/Tic-Tac-Toe/Circle120x120.png')");
        this.#cells.css("background-image", "url('/Tic-Tac-Toe/Circle60x60.png')");
        this.#cells.css("background-image", "");

        this.bindEvents();
        let app = this;
        setTimeout(function () { app.getResultSet(); }, 100);
    }

    bindEvents() {

        let cells = this.#cells;

        for (var i = 0; i < cells.length; i++) {
            $(cells[i]).attr("id", "cell" + i);
            $(cells[i]).attr("data-state", 0);
        }

        $(window).resize(this, function (event) {

            let app = event.data;
            app.refreshUI();
        });

        this.#container.find('[class="btn btn-viewer-nav next"]').off();
        this.#container.find('[class="btn btn-viewer-nav next"]').on('click', this, function (event) {
            let app = event.data;

            let PageNum = parseInt(sessionStorage.getItem(app.InstanceId + "PageNum"));
            let PageCount = parseInt(sessionStorage.getItem(app.InstanceId + "PageCount"));

            if (PageNum + 1 <= PageCount)
                ++PageNum;

            sessionStorage.setItem(app.InstanceId + "PageNum", PageNum + "");

            app.getResultSet.call(app);
        });

        this.#container.find('[class="btn btn-viewer-nav prev"]').off();
        this.#container.find('[class="btn btn-viewer-nav prev"]').on('click', this, function (event) {
            let app = event.data;

            let PageNum = parseInt(sessionStorage.getItem(app.InstanceId + "PageNum"));
            
            if (PageNum - 1 > 0)
                --PageNum;

            sessionStorage.setItem(app.InstanceId + "PageNum", PageNum + "");

            app.getResultSet.call(app);
        });
    }

    refreshUI() {

        let rows = this.#rows;
        let cells = this.#cells;

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
                cell.css("background-image", "url('/Tic-Tac-Toe/Cross" + color + width + "x" + height + ".png')");
            } else if (cellState == 2) {
                cell.css("background-image", "url('/Tic-Tac-Toe/Circle" + color + width + "x" + height + ".png')");
            } else {
                cell.css("background-image", "");
            }
        }

        let gridWidth = ((width + paddingWidth + paddingWidth) * 3);

        let divTicTacToeDescription = this.#container.find("div.tic-tac-toe-description");
        divTicTacToeDescription.html("<div>" + this.#description + "</div>");
        divTicTacToeDescription.width(gridWidth);

        let descriptionDescendantDivs = divTicTacToeDescription.find("div");
        descriptionDescendantDivs.css('color', 'gray');
        descriptionDescendantDivs.css('font-size', screenWidth >= 480 ? '24px' : '12px');
        descriptionDescendantDivs.css('font-weight', screenWidth >= 480 ? 'bold' : 'bold');

        let msg = '';
        
        if (this.#state == 0) {
            msg = '<div style="color: green">In Progress</div>';
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

        let divTicTacToeDisplay = this.#container.find("div.tic-tac-toe-display");
        divTicTacToeDisplay.html(msg);
        divTicTacToeDisplay.width(gridWidth/2);

        let displaDescendantDivs = divTicTacToeDisplay.find("div");
        displaDescendantDivs.css('font-size', screenWidth >= 480 ? '24px' : '12px');
        displaDescendantDivs.css('font-weight', screenWidth >= 480 ? 'bold' : 'bold');

        let PageNum = parseInt(sessionStorage.getItem(this.InstanceId + "PageNum"));
        let PageCount = parseInt(sessionStorage.getItem(this.InstanceId + "PageCount"));
        let divTicTacToePager = this.#container.find("div.tic-tac-toe-pager");
        divTicTacToePager.html("<div>" + PageNum + " of " + PageCount + "</div>");
        divTicTacToePager.width(gridWidth/2);

        let pagerDescendantDivs = divTicTacToePager.find("div");
        pagerDescendantDivs.css('font-size', screenWidth >= 480 ? '24px' : '12px');
        pagerDescendantDivs.css('font-weight', screenWidth >= 480 ? 'bold' : 'bold');
    }

    getResultSet() {

        let unusedCells = this.#cells.filter("[data-state=0]");
        if (unusedCells.length == 0) {
            this.#state = 3;
        }

        let app = this;

        let PageNum = parseInt(sessionStorage.getItem(app.InstanceId + "PageNum"));
        //let PageCount = parseInt(sessionStorage.getItem(app.InstanceId + "PageCount"));

        let thepromise = this.#getTicTacToeResultSetDelegate(1, PageNum);

        thepromise.then(resultSetJson => {

            var resultSet = JSON.parse(resultSetJson);

            //app.log(resultSet);

            sessionStorage.setItem(app.InstanceId + "PageCount", resultSet.PageCount);

            if (resultSet.Results && resultSet.Results.length > 0) {

                let result = resultSet.Results[0];

                app.#description = result.Description;
                app.#state = result.Status;
                app.#winningCells = [];

                //debugger;
                if (result.WinningCells && Array.isArray(result.WinningCells)) {
                    app.#winningCells = result.WinningCells;
                }

                let cellsToChange = $(app.#cells);
                if (result.CellStates && result.CellStates.length == cellsToChange.length) {
                    
                    for (var i = 0; i < cellsToChange.length; i++) {

                        let cellToChange = cellsToChange[i];
                        $(cellToChange).attr("data-state", result.CellStates[i]);
                    }
                }

                app.refreshUI.call(app);
            }
        });
    }

    OnNeedData(funcdelegate) {
        this.#getTicTacToeResultSetDelegate = funcdelegate;
    }
}