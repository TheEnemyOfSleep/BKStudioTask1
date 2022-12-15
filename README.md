# BKStudioTask1
This is simple application to parse expression to the Revers Polish Notation and calculate the result.
Application based on WFP.

# Requirements
To run this application you need to install .net 5.0 runtime.
I didn 't build .net 5 in the release version of the app by default.

# Usage
When you open an Application you will see a three fields:
1. Original Expression
2. Reverse Polish Expression (readonly)
3. Result (readonly)

This application supports the current operands:
- \+ (plus)
- \- or − (minus)
- \/ or ÷ (divide)
- \* or × (multiply)
- \^ (exponentiation)

And can work with real numbers for example:
- 5
- \-5
- 5.5
This numbers shoyld

Also this application supports parenthesis

Expresstion examples:
- 5 * 3 = 15
- 28 / 7 = 4
- 15,2 + 3,5 = 18,7
- 16,3 - 31.7 = -15,4
- \-(5 + 7) = -12
- (3 * 6) * -6 = -108
- 3 + 4 × 2 ÷ ( 1 − 5 ) ^ 2 ^ 3 = 3,0001220703125
