import http from "k6/http";
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";
import { randomIntBetween } from "https://jslib.k6.io/k6-utils/1.2.0/index.js";
const headers = {
  "Content-Type": "application/json",
};

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
  duration: '20m',
  summaryTimeUnit: "ms",
  iterations: 3000
};

const query = `
mutation ($input: Int!) {
  updateParkingValue(value: $input)
}
`;

export default function () {
  let value = randomIntBetween(0, 99999);
  http.post(
    "http://localhost:5000/graphql",
    JSON.stringify({
      query: query,
      variables: {
        input: value,
      },
    }),
    {
      headers: headers,
    }
  );
}

export function handleSummary(data) {
  return {
    "graphqlUpdateParkingValue.html": htmlReport(data),
  };
}
