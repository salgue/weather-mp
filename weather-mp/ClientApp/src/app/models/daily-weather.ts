import { HourlyWeather } from "./hourly-weather";

export interface DailyWeather{
    day: string;
    hourlyForecast: HourlyWeather[];
}