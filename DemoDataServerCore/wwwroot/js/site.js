var report = {
	"dataSource":
	{
		"type": "api",
		"index": "custom-index",
		"url": "/api"
	},
	"slice": {
		rows:
			[
				{ uniqueName: "Country" }
			],
		columns:
			[
				{ uniqueName: "Date" }
			],
		measures:
			[
				{ uniqueName: "Price" }
			]
	}
};

var pivot = new Flexmonster({
	container: "pivotContainer",
	componentFolder: "https://cdn.flexmonster.com/",
	toolbar: true,
	report: report,
	customizeAPIRequest: (req) => {
		req.requestHeaders = {
			"UserToken": $("#country").val()
		};
		return req;
	}
});


$("#country").on("change", function () {
	pivot.clear();
	pivot.setReport(report);
});