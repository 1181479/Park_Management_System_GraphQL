k6 run --out csv=graphqlAllParks.csv graphqlAllParks.js
timeout /t 60
k6 run --out csv=graphqlPartialParks.csv graphqlPartialParks.js
timeout /t 60
k6 run --out csv=graphqlCreateUser.csv graphqlCreateUser.js
timeout /t 60
k6 run --out csv=graphqlLeavePark.csv graphqlLeavePark.js
timeout /t 60
k6 run --out csv=graphqlUpdateParkingValue.csv graphqlUpdateParkingValue.js