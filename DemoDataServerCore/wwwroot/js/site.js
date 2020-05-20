var pivot = new Flexmonster({
	container: "pivotContainer",
	componentFolder: "https://cdn.flexmonster.com/",
	toolbar: true,
	licenseKey: "Z7A5-XADI0U-5Y3U4D-17012V"
});

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

pivot.setReport(report);

$("#country").on("change", function () {
	var userRole = $(this).val();
	pivot.clear();
	switch (userRole) {
		case 'Admin':
			{
				pivot.customizeAPIRequest((req) => {
					req.requestHeaders = {
						"UserToken": "AAAA"
					};
					return req;
				});
				break;
			}
		case 'EU':
			{
				pivot.customizeAPIRequest((req) => {
					req.requestHeaders = {
						"UserToken": "BBBB"
					};
					return req;
				});
				break;
			}
		case 'NA':
			{
				pivot.customizeAPIRequest((req) => {
					req.requestHeaders = {
						"UserToken": "CCCC"
					};
					return req;
				});
				break;
			}
		case 'AU':
			{
				pivot.customizeAPIRequest((req) => {
					req.requestHeaders = {
						"UserToken": "DDDD"
					};
					return req;
				});
				break;
			}
		default:
			{
				break;
			}
	}
	pivot.setReport(report);
});