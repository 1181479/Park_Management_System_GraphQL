library(readr)
files <- list.files(path = "./", pattern = "\\.csv$", full.names = TRUE)

for (file in files) {
  file_name <- tools::file_path_sans_ext(basename(file))
  data <- read_csv(file)
  if(startsWith(file_name,"grpc") && !endsWith(file_name,"Payment")){
    data <- data[data$metric_name == 'grpc_req_duration',]$metric_value
    assign(file_name, data, envir = .GlobalEnv)
  }else if(startsWith(file_name,"graphql") && !endsWith(file_name,"Payment")){
    data <- data[data$metric_name == 'http_req_duration',]$metric_value
    assign(file_name, data, envir = .GlobalEnv)
  }else{
    data <- read_delim(file,delim=";")
    assign(file_name, data$time, envir = .GlobalEnv)
  }
}

rm(data)
rm(file)
rm(files)
rm(file_name)

library(nortest) #LILLIEFORS TEST
library(moments) #SKEWNESS TEST

for(value in ls()){
  #NORMALITY TEST
  assign(paste0(value, "Normality"), lillie.test(get(value))$p.value, envir = .GlobalEnv)
  #ASYMMETRY  TEST
  assign(paste0(value, "Assimtry"), skewness(get(value)), envir = .GlobalEnv)
  rm(value)
}

graphqlLeavePark <- graphqlLeavePark[1:3000]
grpcLeavePark <- grpcLeavePark[1:3000]
grpcLeaveParkClientStream <- grpcLeaveParkClientStream[1:3000]
grpcLeaveParkServerStream <- grpcLeaveParkServerStream[1:3000]
grpcLeaveParkServerStreamPayment <- grpcLeaveParkServerStreamPayment[1:3000]
grpcLeaveParkTwoSideStream <- grpcLeaveParkTwoSideStream[1:3000]


#Average
graphqlLeaveParkPaymentMean <- mean(graphqlLeaveParkPayment)
grpcLeaveParkPaymentMean <- mean(grpcLeaveParkPayment)
grpcLeaveParkClientStreamPaymentMean <- mean(grpcLeaveParkClientStreamPayment)
grpcLeaveParkServerStreamPaymentMean <- mean(grpcLeaveParkServerStreamPayment)
grpcLeaveParkTwoSideStreamPaymentMean <- mean(grpcLeaveParkTwoSideStreamPayment)

#Maximum
graphqlLeaveParkPaymentMax <- max(graphqlLeaveParkPayment)
grpcLeaveParkPaymentMax <- max(grpcLeaveParkPayment)
grpcLeaveParkClientStreamPaymentMax <- max(grpcLeaveParkClientStreamPayment)
grpcLeaveParkServerStreamPaymentMax <- max(grpcLeaveParkServerStreamPayment)
grpcLeaveParkTwoSideStreamPaymentMax <- max(grpcLeaveParkTwoSideStreamPayment)

#Median
graphqlLeaveParkPaymentMedian <- median(graphqlLeaveParkPayment)
grpcLeaveParkPaymentMedian <- median(grpcLeaveParkPayment)
grpcLeaveParkClientStreamPaymentMedian <- median(grpcLeaveParkClientStreamPayment)
grpcLeaveParkServerStreamPaymentMedian <- median(grpcLeaveParkServerStreamPayment)
grpcLeaveParkTwoSideStreamPaymentMedian <- median(grpcLeaveParkTwoSideStreamPayment)

#Minimum
graphqlLeaveParkPaymentMin <- min(graphqlLeaveParkPayment)
grpcLeaveParkPaymentMin <- min(grpcLeaveParkPayment)
grpcLeaveParkClientStreamPaymentMin <- min(grpcLeaveParkClientStreamPayment)
grpcLeaveParkServerStreamPaymentMin <- min(grpcLeaveParkServerStreamPayment)
grpcLeaveParkTwoSideStreamPaymentMin <- min(grpcLeaveParkTwoSideStreamPayment)

#90th Percentile 	95th Percentile
graphqlLeaveParkPaymentPercentiles <- quantile(graphqlLeaveParkPayment, probs = seq(.9, .95, by = .05))
grpcLeaveParkPaymentPercentiles <- quantile(grpcLeaveParkPayment, probs = seq(.9, .95, by = .05))
grpcLeaveParkClientStreamPaymentPercentiles <- quantile(grpcLeaveParkClientStreamPayment, probs = seq(.9, .95, by = .05))
grpcLeaveParkServerStreamPaymentPercentiles <- quantile(grpcLeaveParkServerStreamPayment, probs = seq(.9, .95, by = .05))
grpcLeaveParkTwoSideStreamPaymentPercentiles <- quantile(grpcLeaveParkTwoSideStreamPayment, probs = seq(.9, .95, by = .05))

# GET ALL PARKS
grpcAllParks_graphqlAllParks <- wilcox.test(grpcAllParks,graphqlAllParks, paired=FALSE)$p.value
grpcAllParksClientStream_graphqlAllParks <- wilcox.test(grpcAllParksClientStream,graphqlAllParks, paired=FALSE)$p.value
grpcAllParksServerStream_graphqlAllParks <- wilcox.test(grpcAllParksServerStream,graphqlAllParks, paired=FALSE)$p.value
grpcAllParksTwoSidedStream_graphqlAllParks <- wilcox.test(grpcAllParksTwoSidedStream,graphqlAllParks, paired=FALSE)$p.value

# CREATE USER
grpcCreateUser_graphqlCreateUser <- wilcox.test(grpcCreateUser,graphqlCreateUser, paired=FALSE)$p.value

# GET PARTIAL PARKS
grpcPartialParks_graphqlPartialParks <- wilcox.test(grpcPartialParks,graphqlPartialParks, paired=FALSE)$p.value

# UPDATE PARKING VALUE
grpcUpdateParkingValue_graphqlUpdateParkingValue <- wilcox.test(grpcUpdateParkingValue,graphqlUpdateParkingValue, paired=FALSE)$p.value

# LEAVE PARK
grpcLeavePark_graphqlLeavePark <- wilcox.test(grpcLeavePark,graphqlLeavePark, paired=FALSE)$p.value
grpcLeaveParkClientStream_graphqlLeavePark <- wilcox.test(grpcLeaveParkClientStream,graphqlLeavePark, paired=FALSE)$p.value
grpcLeaveParkServerStream_graphqlLeavePark <- wilcox.test(grpcLeaveParkServerStream,graphqlLeavePark, paired=FALSE)$p.value
grpcLeaveParkTwoSideStream_graphqlLeavePark <- wilcox.test(grpcLeaveParkTwoSideStream,graphqlLeavePark, paired=FALSE)$p.value

# LEAVE PARK PAYMENT
grpcLeaveParkPayment_graphqlLeaveParkPayment <- wilcox.test(grpcLeaveParkPayment,graphqlLeaveParkPayment, paired=FALSE)$p.value
grpcLeaveParkClientStreamPayment_graphqlLeaveParkPayment <- wilcox.test(grpcLeaveParkClientStreamPayment,graphqlLeaveParkPayment, paired=FALSE)$p.value
grpcLeaveParkServerStreamPayment_graphqlLeaveParkPayment <- wilcox.test(grpcLeaveParkServerStreamPayment,graphqlLeaveParkPayment, paired=FALSE)$p.value
grpcLeaveParkTwoSideStreamPayment_graphqlLeaveParkPayment <- wilcox.test(grpcLeaveParkTwoSideStreamPayment,graphqlLeaveParkPayment, paired=FALSE)$p.value

files <- list.files(path = "./", pattern = "\\.csv$", full.names = TRUE)

for (file in files) {
  for(value in ls()){
    if(value==tools::file_path_sans_ext(basename(file))){
      rm(list = value)
    }
  }
}
rm(file)
rm(files)
rm(value)

save(list = ls(), file = "./results.RData")
