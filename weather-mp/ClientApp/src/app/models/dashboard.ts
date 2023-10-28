import { WeeklyWeather } from "./weekly-weather";

export interface Dashboard {
    city: string;
    weathers: WeeklyWeather[];
}