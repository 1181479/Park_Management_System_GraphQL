import http from "k6/http";
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";
const headers = {
  "Content-Type": "application/json",
};
const addTokenMutation = `
mutation ($token: String!){
  addToken(token: $token)
}
`;

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
  duration: '2h',
  summaryTimeUnit: "ms",
  iterations: 3000
};

export function setup() {
  http.post(
    "http://localhost:5004/graphql",
    JSON.stringify({
      query: addTokenMutation,
      variables: {
        token: "tok_123456789",
      },
    }),
    {
      headers: headers,
    }
  );
}

const leaveParkMutation = `
mutation ($input: ParkingSpotsUpdateRequestDtoInput!) {
  leavePark(requestDto: $input) {
    isSuccessfull
    parkyCoinsAmount
    otherPaymentMethodAmount
    totalCost
  }
}
`;

export default function () {
  http.post(
    "http://localhost:5000/graphql",
    JSON.stringify({
      query: leaveParkMutation,
      variables: {
        input: {
          isEntrance: false,
          licensePlate: "ABC123",
          parkName: "Central Park",
        },
      },
    }),
    {
      headers: headers,
    }
  );
}

export function handleSummary(data) {
  return {
    "graphqlLeavePark.html": htmlReport(data),
  };
}
