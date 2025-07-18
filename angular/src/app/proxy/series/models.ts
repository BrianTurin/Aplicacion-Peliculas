import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateSerieDto {
  title?: string;
  year?: string;
  poster?: string;
  released?: string;
  imdbId?: string;
  ImdbRating?: string;
  ImdbVotos: number;
  seasons: SeasonDto[];
  ratings: RatingDto[];
  duration?: string;
  genre?: string;
  director?: string;
  writer?: string;
  actors?: string;
  plot?: string;
  languaje?: string;
  country?: string;
  type?: string;
  numSeasons: number;
}

export interface SerieDto extends EntityDto<number> {
  title?: string;
  year?: string;
  poster?: string;
  released?: string;
  imdbId?: string;
  ImdbRating?: string;
  ImdbVotos: number;
  seasons: SeasonDto[];
  ratings: RatingDto[];
  duration?: string;
  genre?: string;
  director?: string;
  writer?: string;
  actors?: string;
  plot?: string;
  languaje?: string;
  country?: string;
  type?: string;
  numSeasons: number;
}

export interface SeasonDto extends EntityDto<number>{
  serieID: number;
  seasonNumber: number;
  title?: string;
  episodes: EpisodeDto[];
  released?: string;
}

export interface EpisodeDto extends EntityDto<number>{
  seasonID: number;
  episodeNumber: number;
  title?: string;
  duration?: string;
  released?: string;
  resume?: string;
  writer?: string;
  director?: string;
}

export interface RatingDto extends EntityDto<number>{
  serieID: string;
  score: number;
  commentary?: string;
  userID: number;
  createdDate: string;
}

export interface APIMonitoringDto extends EntityDto<number> {
  StartTime?: string;
  EndTime?: string;
  TotalTime: number;
  Events: string[];
}
export interface APIStatisticsDto extends EntityDto<number> {
  AverageDuration: number;
  NumEvents: number; 
  NumMonitorings: number;
}