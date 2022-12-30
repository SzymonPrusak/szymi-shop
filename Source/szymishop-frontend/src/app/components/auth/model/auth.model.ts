
export interface RefreshToken {
    id: string,
    signature: string
};

export interface AuthTokens {
    accessToken: string,
    refreshToken: RefreshToken
};
