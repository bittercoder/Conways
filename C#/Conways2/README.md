This is an implementation of conways game of life where I attempted the code-retreat exercise of "Verbs instead of Nouns" - things of interest:

* You potentially end up with a lot more classes, but the classes are smaller.
* At first glance it would appear the solution would lend itself to replacing the various classes with functions, and taking a functional approach to implementation.
* The Cell and Cell List are bereft of behaviour.  Though the exercise is verb's instead of nouns, I'm not to sure how you would eliminate the need for a noun-named class to represent your data - though if you review Point, Cell and CellList - this could in fact be replaced with something like:

    List<Tuple<Tuple<int,int>,bool>>

e.g. List of tuples representing cells, which in turn uses a tuple to represent the point the cell exists at.