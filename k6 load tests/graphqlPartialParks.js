import http from "k6/http";
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

// Define the load test configuration
export const options = {
  summaryTrendStats: [
    "avg", //average
    "min",
    "med", //median
    "max",
    "p(50)",
    "p(90)",
    "p(95)",
    "p(99)",
    "p(99.99)",
    "count",
  ],
  summaryTimeUnit: "ms",
  stages: [
    { duration: "1m", target: 1 },
    { duration: "2m", target: 10 },
    { duration: "3m", target: 50 },
  ],
};

const query = `
query AllParks {
  allParks {
    id
    numberFloors
    parkName
    latitude
    longitude
    location
    openingTime
    closingTime
    isActive
    nightFee
  }
}
`;

export default function () {
  const headers = {
    "Content-Type": "application/json",
  };

  const res = http.post(
    "http://localhost:5000/graphql",
    JSON.stringify({ query: query }),
    {
      headers: headers,
    }
  );
}

export function handleSummary(data) {
  return {
    "graphqlPartialParks.html": htmlReport(data),
  };
}
