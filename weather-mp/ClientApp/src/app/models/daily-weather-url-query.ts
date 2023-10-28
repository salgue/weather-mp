import { WeeklyWeatherUrlQuery } from "./weekly-weather-url-query";

export interface DailyWeatherUrlQuery extends WeeklyWeatherUrlQuery{
    searchDate: string;
}