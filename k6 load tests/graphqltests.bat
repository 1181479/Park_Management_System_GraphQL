k6 run graphqlAllParks.js --out web-dashboard=export=graphqlAllParks-test-report.html
k6 run graphqlPartialParks.js --out web-dashboard=export=graphqlPartialParks-test-report.html
k6 run graphqlCreateUser.js --out web-dashboard=export=graphqlCreateUser-test-report.html