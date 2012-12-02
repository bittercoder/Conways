Here is a fairly standard implementation of conways game of life for C#.

Probably the interesting thing about this implementation is that the world memoizes the count of neighbour cells and uses paralellization to speed up the "tick" process, this logic all falls within the "World" implementation.