/* =========================================================================
   Reusable confirmation modal for the admin panel.
   The modal markup lives once in _AdminLayout.cshtml (#confirmModal).

   Two ways to use it:
   1. Declarative (navigation deletes): add to any link/button
        data-confirm="Are you sure you want to delete this book?"
        data-href="/Admin/DeleteBook?BookID=5"
      A click opens the modal and, on confirm, navigates to data-href.

   2. Programmatic (AJAX deletes or custom actions):
        confirmAction('Delete this feedback?', function () { DeleteFeedback(id); });
   ========================================================================= */
(function () {
    var pending = null;

    function modal() { return document.getElementById('confirmModal'); }

    window.confirmAction = function (message, onConfirm, opts) {
        var m = modal();
        if (!m) { if (window.confirm(message ? message.replace(/<[^>]+>/g, '') : 'Are you sure?')) { onConfirm(); } return; }
        opts = opts || {};
        document.getElementById('confirmModalText').innerHTML = message || 'Are you sure?';
        document.getElementById('confirmModalBtn').innerHTML =
            '<i class="fa ' + (opts.icon || 'fa-trash-o') + '"></i> ' + (opts.confirmLabel || 'Delete');
        pending = onConfirm;
        m.style.display = 'block';
    };

    window.closeConfirmModal = function () {
        var m = modal();
        if (m) { m.style.display = 'none'; }
        pending = null;
    };

    function init() {
        var btn = document.getElementById('confirmModalBtn');
        if (btn) {
            btn.addEventListener('click', function () {
                var fn = pending;
                window.closeConfirmModal();
                if (typeof fn === 'function') { fn(); }
            });
        }

        // Declarative deletes: any element with data-confirm.
        document.addEventListener('click', function (e) {
            var el = e.target.closest ? e.target.closest('[data-confirm]') : null;
            if (!el) { return; }
            e.preventDefault();
            var href = el.getAttribute('data-href');
            window.confirmAction(el.getAttribute('data-confirm'), function () {
                if (href) { window.location.href = href; }
            });
        });

        window.addEventListener('click', function (e) {
            if (e.target === modal()) { window.closeConfirmModal(); }
        });
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape') { window.closeConfirmModal(); }
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
