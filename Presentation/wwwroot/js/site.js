// YourDelivery — UI behaviour

(function () {
	const STORAGE_KEY = "yd-sidebar-collapsed";

	// Restore collapsed state as early as possible to avoid layout flashing.
	if (localStorage.getItem(STORAGE_KEY) === "true") {
		document.body.classList.add("sidebar-collapsed");
	}

	document.addEventListener("DOMContentLoaded", function () {
		const toggle = document.getElementById("sidebarToggle");
		if (toggle) {
			toggle.addEventListener("click", function () {
				const collapsed = document.body.classList.toggle("sidebar-collapsed");
				localStorage.setItem(STORAGE_KEY, collapsed);
			});
		}
	});
})();
