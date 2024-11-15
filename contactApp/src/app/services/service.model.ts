export function isMwsResponse(
  response: any
): response is WebServiceResponse<any> {
  return (
    response &&
    response.Header !== undefined &&
    response.Header.StatusCode !== undefined &&
    typeof response.Header.StatusCode === 'number'
  );
}

export function isMwsResponseError(
  response: any
): response is WebServiceResponseError {
  return response && typeof response.message === 'string';
}

export function getMessagesFromHeader(
  httpResponse: WebServiceResponse<any>,
  httpStatusCode: number
): Array<ResponseMessage> {
  if (isMwsResponse(httpResponse)) {
    return httpResponse.Header.Messages.filter(
      (element) => element.StatusCode === httpStatusCode
    );
  }

  return [];
}

export class WebServiceResponse<T> {
  Body!: T;
  Header!: ResponseHeader;
  Message!: string;
  Errors!: any;
  constructor(items?: WebServiceResponse<T>) {
    if (items) {
      this.Body = items.Body;
      this.Header = items.Header;
    }
  }
}

export class WebServiceResponseError {
  displayMessage?: string;
  message!: string;
  statusCode?: number;
  constructor(item?: WebServiceResponseError) {
    if (item) {
      this.displayMessage = item.displayMessage;
      this.message = item.message;
      this.statusCode = item.statusCode;
    }
  }
}

export class ResponseHeader {
  Messages!: Array<ResponseMessage>;
  PagingInfo?: PagingInfo;
  Date!: string;
  Status!: string;
  StatusCode!: number;
  constructor(items?: ResponseHeader) {
    if (items) {
      this.Date = items.Date;
      this.Status = items.Status;
      this.StatusCode = items.StatusCode;
      this.PagingInfo = items.PagingInfo
        ? new PagingInfo(items.PagingInfo)
        : undefined;

      if (items.Messages !== undefined && items.Messages.length > 0) {
        this.Messages = items.Messages.map(
          (message) => new ResponseMessage(message)
        );
      }
    }
  }
}

export class ResponseMessage {
  StatusCode?: number;
  Code?: string;
  ShortDescription?: string;
  LongDescription?: string;
  Type?: string;
  constructor(items?: ResponseMessage) {
    if (items) {
      this.StatusCode = items.StatusCode;
      this.Code = items.Code;
      this.ShortDescription = items.ShortDescription;
      this.LongDescription = items.LongDescription;
      this.Type = items.Type;
    }
  }
}

export class PagingInfo {
  EndIndex?: number;
  ItemsPerPage?: number;
  StartIndex?: number;
  TotalItemCount?: number;
  Links?: Array<Link>;
  constructor(items?: PagingInfo) {
    if (items) {
      this.EndIndex = items.EndIndex;
      this.ItemsPerPage = items.ItemsPerPage;
      this.StartIndex = items.StartIndex;
      this.TotalItemCount = items.TotalItemCount;
      if (items.Links !== undefined && items.Links.length > 0) {
        this.Links = items.Links.map((link) => new Link(link));
      }
    }
  }
}

export class Link {
  Href?: string;
  Rel?: string;
  constructor(items?: Link) {
    if (items) {
      this.Href = items.Href;
      this.Rel = items.Rel;
    }
  }
}
export enum WithRel {
  EWT = 'EWT',
}

export enum UnauthorizedMessages {
  'e0' = 'Your session has expired, please login again.',
  'e1' = 'You have been logged out due to this account being logged in on a different device.',
}

export enum RequestMethod {
  Get,
  Post,
  Put,
  Delete,
  Options,
  Head,
  Patch,
}
