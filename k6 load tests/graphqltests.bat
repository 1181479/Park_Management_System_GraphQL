k6 run --insecure-skip-tls-verify --out csv=graphqlAllParks.csv graphqlAllParks.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=graphqlPartialParks.csv graphqlPartialParks.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=graphqlCreateUser.csv graphqlCreateUser.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=graphqlLeavePark.csv graphqlLeavePark.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=graphqlUpdateParkingValue.csv graphqlUpdateParkingValue.js