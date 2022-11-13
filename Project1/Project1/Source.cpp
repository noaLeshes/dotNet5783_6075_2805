#include <iostream>
using namespace std;
int main()
//auther: Noa Leshes 325352805
//Course: Mavo Lemadaey hamachshev
//targil 2 exrecise 8
//the program gets ditails about a flight: the time of the takeoff, the enter flight duration and calculatse the time of the landing.
{
	int seconds, minutes, hours, seconds1, minutes1, hours1, allSeconds, allMinutes, allHours;
	int sumSeconds, sumMinutes;
	cout << "enter flight takeoff:" << endl;
	cin >> hours >> minutes >> seconds;
	cout << "enter flight duration:" << endl;
	cin >> hours1 >> minutes1 >> seconds1;
	allSeconds = (seconds + seconds1) % 60;
	sumSeconds = (seconds + seconds1) / 60;
	allMinutes = (minutes + minutes1 + sumSeconds) % 60;
	sumMinutes = (minutes + minutes1) / 60;
	allHours = (hours + hours1 + sumMinutes) % 24;
	cout << "flight arrival is:" << endl << allHours << ":" << allMinutes << ":" << allSeconds << endl;

	return 0;
}