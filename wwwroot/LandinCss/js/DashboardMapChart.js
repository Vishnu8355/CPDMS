
		ZC.LICENSE = ["569d52cefae586f634c54f86dc99e6a9", "b55b025e438fa8a98e32482b5f768ff5"];
		let chartConfig = {
			shapes: [{
				type: 'zingchart.maps',
				options: {
					bbox: [67.177, 36.494, 98.403, 6.965], // get bbox from zingchart.maps.getItemInfo('world-countries','ind');
					ignore: ['IND'], // ignore India because we are rendering a more specific India map below
					name: 'world.countries',
					panning: false, // turn of zooming. Doesn't work with bounding box
					style: {
						tooltip: {
							borderColor: '#000',
							borderWidth: '2px',
							fontSize: '18px'
						},
						controls: {
							visible: false // turn of zooming. Doesn't work with bounding box
						},
						hoverState: {
							alpha: .28
						}
					},
					zooming: false // turn of zooming. Doesn't work with bounding box
				}
			},
			{
				type: 'zingchart.maps',
				options: {
					name: 'ind',
					panning: false, // turn of zooming. Doesn't work with bounding box
					zooming: false,
					scrolling: false,
					style: {
						tooltip: {
							borderColor: '#000',
							borderWidth: '2px',
							fontSize: '18px'
						},
						borderColor: '#000',
						borderWidth: '2px',
						controls: {
							visible: false, // turn of zooming. Doesn't work with bounding box

						},
						hoverState: {
							alpha: .28
						},
						items: {
							KA: {
								tooltip: {
									text: 'Karnataka has 2,851 monthly users total',
									backgroundColor: '#ff5722'
								},
								backgroundColor: '#ff5722',
								label: {
									visible: true
								}
							},
							MH: {
								tooltip: {
									text: 'Maharashtra has 2,683 monthly users total',
									backgroundColor: '#ff9800'
								},
								backgroundColor: '#ff9800',
								label: {
									visible: true
								}
							},
							TL: {
								tooltip: {
									text: 'Telangana has 1,494 monthly users total',
									backgroundColor: '#00AE4D'
								},
								backgroundColor: '#00AE4D',
								label: {
									visible: true
								}
							},
							TN: {
								tooltip: {
									text: 'Tamil Nadu has 1,968 monthly users total',
									backgroundColor: '#00bcd4'
								},
								backgroundColor: '#00bcd4',
								label: {
									visible: true
								}
							}
						},
						label: { // text displaying. Like valueBox
							fontSize: '15px',
							visible: false
						}
					}
				}
			}
			]
		};

		zingchart.loadModules('maps,maps-ind,maps-world-countries');
		zingchart.render({
			id: 'myChart',
			data: chartConfig,
			height: '100%',
			width: '100%',
		});
	