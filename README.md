## 100 PRISONERS

## The SetUp
There are 100 prisoners numbered from 1 to 100.
Slips of paper containing each of their numbers (1 - 100) are randomly placed in 100 boxes in a sealed room.
One at a time, each prisoner is allowed to enter the room and open up to 50 boxes looking for their number, once they find it they must leave the room as they found it.
They cant talk in any way with the other prisoners.

## Bullet Points

* If all of the prisoners find their own number all of them will be freed.
* If even one of them fails all of them will be sentence to death and executed.

## Loops 
A loop is the number of attemtps that a prisoner takes to find its own number.
Each box in the sealed room become a part of a closed loop, the simpliest loop is a box that contains its own number.
The longest loop will connect all the boxes pointing to the next one.

Eventually this will result in a mix of random different lenght loops.

When a prisoner starts with his own number box he is guarantedd that he is in a loop that contains a box with his number.
So what defines if the prisoner finds or not his own number is the lenght of the loop (up to 50 boxes) such that the loop length has to be less or equals to 50


Example: a loop of 1
 P1
	[1]
	1

Example: a loop of 6 
 P56 
	[56] -> [2] -> [98] -> [41] -> [81] -> [6] -> [56]
	2		98		41		81		6		56


## Possible Strategies

* All of them decides to look for his number randombly.
	- The probability that each of the prisoners has is 50% chances to find his own number.
	- Such as the probability of Success of the 100 prisoners is 1/2 x 1/2 x 1/2 ... efectively (1/2) ^ 100 = 7.888609052210118e-31 = 0.00000000000000000000000000000078%)

* All of them start by picking their own number and  then go to the box with that number on it and so on until they find their own number.
	- The probability of all the prisoners find their number is up to 31%
	- So the probability of all the prisoners to succeed is equals to the probability of a random arrengement of 100 numbers contains no loops longer than 50. ~1/3 
	
# Calculate the strategy
	
How many loops can we have in an arrengement of 100 numbers?

The total number of permutations would be 100x99x98x97...x1, effectively 100! 

There is something to take into account
A loop like [1] -> [2] -> [3] -> [4] -> ... [100]

its the exact same as a loop like [2] -> [3] -> [4] -> [5] ... -> [100] -> [1]

so the number of unique loops is 100!/100

Now we have a formula for the probability of having a loop of lenght 100

P(L=100) = #unique loops / total permutaions
P(L=100) = (100!/100) / 100!
P(L=100) = (1/100)

this formula generates a general result, it can be applied such as 

P(L=99) = (100!/99) / 100!
P(L=99) = (1/99)

.
.
.

such as the probability of getting a loop with length longer that 50 is: 1/51 + 1/52 + 1/53 ... 1/100 effectively 0.6881721793101949
that number can be interpretated as there are 69% chances of failure of the prisoners, then there is 31% chances of each prisoner to succeed.

But the chance of every individual prisoner alone to find his number following the loops strategy is still 50% each one still only can open only half the boxes so their indivual chance is still 1/2.
but this chances are no longer independant of each other.

For this part I need to add some graphs to see how the strategy works.

# The Question
How it is guaranteed that if a prisoner starts on his own number box he will be inside a loop that contains his own number? 

so every slip with a number of prisoner and a box with the same number form a unit [6] <=> 6 with that in mind we can check some key poins here

* what would happen if willingly create loops longer that 51? whould that strategy hold up? 
	- allegedly its possible to break loops bigger that 50 legth by swaping teh contents of 2 random boxes.
	- by arbitrarily renumbering the boxes (adding 2 to the original number) that essencialy the same as redestributing the slips meaning that prisoners are back to a random arrange of loops
	
* what happens if you increase the number of prisoners?
	- the probability happens to decrease (it has a limit) after each increment of prisoners: some graphs will be cool.
	


