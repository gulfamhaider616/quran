/* =========================================================================
   Reusable search / filter for any list of records coming from the database.
   Two modes:
   1. Auto-enhance: every <table class="rest"> inside the admin panel gets a
      search box injected above it that filters its <tbody> rows by text.
   2. Explicit: any <input data-search-target="#selector" data-search-items=".item">
      filters the matching items inside the target container (used on public
      card / list pages). Optionally point data-search-empty at an element to
      toggle when nothing matches.
   ========================================================================= */
(function () {
    function norm(s) { return (s || '').toLowerCase().replace(/\s+/g, ' ').trim(); }

    /* ---- 1. Admin data tables (.rest) ---------------------------------- */
    function enhanceTables() {
        if (!document.body || !document.body.classList.contains('admin-body')) { return; }
        var tables = document.querySelectorAll('table.rest');
        Array.prototype.forEach.call(tables, function (table) {
            if (table.getAttribute('data-search-done') || !table.tBodies.length) { return; }
            var tbody = table.tBodies[0];
            var rows = Array.prototype.filter.call(tbody.rows, function (r) {
                return !(r.cells.length === 1 && r.cells[0].hasAttribute('colspan'));
            });
            if (rows.length === 0) { return; }
            table.setAttribute('data-search-done', '1');

            var colCount = rows[0].cells.length;

            var wrap = document.createElement('div');
            wrap.className = 'tbl-search';
            wrap.innerHTML =
                '<span class="tbl-search-field"><i class="fa fa-search"></i>' +
                '<input type="text" placeholder="Search this list..." autocomplete="off" /></span>' +
                '<span class="tbl-search-count"></span>';

            // Place the search on the same line as the page's action button.
            // Reuse an existing .adm-toolbar (with the Add button) or create one.
            var scope = (table.closest && table.closest('#banner')) || document;
            var toolbar = scope.querySelector('.adm-toolbar');
            if (!toolbar) {
                toolbar = document.createElement('div');
                toolbar.className = 'adm-toolbar';
                table.parentNode.insertBefore(toolbar, table);
            }
            toolbar.insertBefore(wrap, toolbar.firstChild);

            var input = wrap.querySelector('input');
            var count = wrap.querySelector('.tbl-search-count');

            var emptyRow = document.createElement('tr');
            emptyRow.className = 'tbl-search-empty';
            emptyRow.style.display = 'none';
            emptyRow.innerHTML = '<td colspan="' + colCount + '">No matching records found.</td>';
            tbody.appendChild(emptyRow);

            input.addEventListener('input', function () {
                var q = norm(input.value);
                var shown = 0;
                rows.forEach(function (r) {
                    var match = !q || norm(r.textContent).indexOf(q) > -1;
                    r.style.display = match ? '' : 'none';
                    if (match) { shown++; }
                });
                emptyRow.style.display = shown ? 'none' : '';
                count.textContent = q ? (shown + (shown === 1 ? ' result' : ' results')) : '';
            });
        });
    }

    /* ---- 2. Explicit inputs (public card / list pages) ----------------- */
    function enhanceCustom() {
        var inputs = document.querySelectorAll('[data-search-target]');
        Array.prototype.forEach.call(inputs, function (input) {
            if (input.getAttribute('data-search-done')) { return; }
            input.setAttribute('data-search-done', '1');
            var container = document.querySelector(input.getAttribute('data-search-target'));
            if (!container) { return; }
            var sel = input.getAttribute('data-search-items') || '*';
            var items = Array.prototype.slice.call(container.querySelectorAll(sel));
            var emptySel = input.getAttribute('data-search-empty');
            var emptyEl = emptySel ? document.querySelector(emptySel) : null;

            input.addEventListener('input', function () {
                var q = norm(input.value);
                var shown = 0;
                items.forEach(function (it) {
                    var hay = norm(it.getAttribute('data-search') || it.textContent);
                    var match = !q || hay.indexOf(q) > -1;
                    it.style.display = match ? '' : 'none';
                    if (match) { shown++; }
                });
                if (emptyEl) { emptyEl.style.display = shown ? 'none' : 'block'; }
            });
        });
    }

    function init() { enhanceTables(); enhanceCustom(); }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
