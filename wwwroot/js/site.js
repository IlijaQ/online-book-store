console.log(`
|   |        |  |
|___|   __   |  |   __
|   |  |__|  |  |  |  |
|   |  |__   |  |  |__|

`);

let currentWeekDay = new Date().toLocaleString('en-US', { weekday: 'long' });
let timeOfDay = null;
let currentHour = new Date().getHours();

if (currentHour < 12) {
    timeOfDay = 'morning';
}
else if (currentHour < 20) {
    timeOfDay = 'afternoon';
}
else if (currentHour < 22) {
    timeOfDay = 'evening';
}
else {
    timeOfDay = 'night';
}

console.log(`How are you on this fine ${currentWeekDay} ${timeOfDay}?`);