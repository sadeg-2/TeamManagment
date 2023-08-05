
//const greenBackground = "linear-gradient(to right, #00b09b, #96c93d)";
//showToastWithBackground("Add Comment Successfully", greenBackground);

function showToastWithBackground(text, background) {
	Toastify({
		text: text,
		duration: 5000,
		newWindow: true,
		close: true,
		gravity: "top",
		position: "right",
		stopOnFocus: true,
		style: {
			background: background,
		},
		onClick: function () { }
	}).showToast();
}